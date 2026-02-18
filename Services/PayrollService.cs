using AutoMapper;
using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;

namespace HRMS.Backend.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _repository;
        private readonly ISalaryRepository _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        private const decimal TAX_PERCENTAGE = 0.10m; 

        public PayrollService(
            IPayrollRepository repository,
            ISalaryRepository salaryRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _repository = repository;
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // Create payroll for single employee
        public async Task<PayrollDTO> CreateAsync(PayrollCreateDTO dto)
        {
            var existing = await _repository.GetByEmployeeAndMonthAsync(dto.EmployeeId, dto.PayrollMonth);
            if (existing != null)
                throw new Exception("Payroll for this employee and month already exists.");

            var salaries = await _salaryRepository.GetByEmployeeIdAsync(dto.EmployeeId);
            var latestSalary = salaries.OrderByDescending(s => s.EffectiveFrom).FirstOrDefault();
            if (latestSalary == null)
                throw new Exception("Salary not found for this employee.");

            decimal totalSalary = latestSalary.BasicSalary + latestSalary.Bonus - latestSalary.Deduction;

            // Calculate tax automatically
            decimal tax = totalSalary * TAX_PERCENTAGE;
            decimal netSalary = totalSalary - tax;

            var payroll = new Payroll
            {
                EmployeeId = dto.EmployeeId,
                TotalSalary = totalSalary,
                Tax = tax,
                NetSalary = netSalary,
                PayrollMonth = dto.PayrollMonth,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(payroll);
            return _mapper.Map<PayrollDTO>(created);
        }

        // Get all payrolls
        public async Task<IEnumerable<PayrollDTO>> GetAllAsync()
        {
            var payrolls = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<PayrollDTO>>(payrolls);
        }

        // Get payroll by id
        public async Task<PayrollDTO?> GetByIdAsync(int id)
        {
            var payroll = await _repository.GetByIdAsync(id);
            return payroll == null ? null : _mapper.Map<PayrollDTO>(payroll);
        }

        // Update payroll
        public async Task<PayrollDTO?> UpdateAsync(int id, PayrollUpdateDTO dto)
        {
            var payroll = await _repository.GetByIdAsync(id);
            if (payroll == null) return null;

            var salaries = await _salaryRepository.GetByEmployeeIdAsync(payroll.EmployeeId);
            var latestSalary = salaries.OrderByDescending(s => s.EffectiveFrom).FirstOrDefault();
            if (latestSalary != null)
                payroll.TotalSalary = latestSalary.BasicSalary + latestSalary.Bonus - latestSalary.Deduction;

            payroll.Tax = payroll.TotalSalary * TAX_PERCENTAGE;
            payroll.NetSalary = payroll.TotalSalary - payroll.Tax;

            if (dto.PayrollMonth.HasValue)
                payroll.PayrollMonth = dto.PayrollMonth.Value;

            payroll.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(payroll);
            return _mapper.Map<PayrollDTO>(updated);
        }

        // Delete payroll
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // Generate payrolls for all employees for a given month
        public async Task<IEnumerable<PayrollDTO>> GenerateMonthlyPayrollsAsync(DateTime month)
        {
            // Get all employees
            var employees = await _employeeRepository.GetAllAsync();
            var createdPayrolls = new List<Payroll>();

            foreach (var emp in employees)
            {
                // Skip if payroll already exists
                var existing = await _repository.GetByEmployeeAndMonthAsync(emp.Id, month);
                if (existing != null) continue;

                // Get latest salary
                var salaries = await _salaryRepository.GetByEmployeeIdAsync(emp.Id);
                var latestSalary = salaries.OrderByDescending(s => s.EffectiveFrom).FirstOrDefault();
                if (latestSalary == null) continue;

                decimal totalSalary = latestSalary.BasicSalary + latestSalary.Bonus - latestSalary.Deduction;
                decimal tax = totalSalary * TAX_PERCENTAGE;
                decimal netSalary = totalSalary - tax;

                var payroll = new Payroll
                {
                    EmployeeId = emp.Id,
                    TotalSalary = totalSalary,
                    Tax = tax,
                    NetSalary = netSalary,
                    PayrollMonth = month,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var created = await _repository.AddAsync(payroll);
                createdPayrolls.Add(created);
            }

            return _mapper.Map<IEnumerable<PayrollDTO>>(createdPayrolls);
        }
    }
}
