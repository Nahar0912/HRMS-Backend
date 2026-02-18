using AutoMapper;
using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;


namespace HRMS.Backend.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _repository;
        private readonly IPayrollRepository _payrollRepository;
        private readonly IMapper _mapper;

        public SalaryService(ISalaryRepository repository, IPayrollRepository payrollRepository, IMapper mapper)
        {
            _repository = repository;
            _payrollRepository = payrollRepository;
            _mapper = mapper;
        }

        public async Task<SalaryDTO> CreateAsync(SalaryCreateDTO dto)
        {
            var salary = _mapper.Map<Salary>(dto);
            salary.CreatedAt = DateTime.UtcNow;
            salary.UpdatedAt = DateTime.UtcNow;

            var created = await _repository.AddAsync(salary);

            // Update payroll if exists for this month
            await UpdatePayrollForEmployee(created.EmployeeId);

            return _mapper.Map<SalaryDTO>(created);
        }

        public async Task<SalaryDTO?> UpdateAsync(int id, SalaryUpdateDTO dto)
        {
            var salary = await _repository.GetByIdAsync(id);
            if (salary == null) return null;

            if (dto.BasicSalary.HasValue) salary.BasicSalary = dto.BasicSalary.Value;
            if (dto.Bonus.HasValue) salary.Bonus = dto.Bonus.Value;
            if (dto.Deduction.HasValue) salary.Deduction = dto.Deduction.Value;
            if (dto.EffectiveFrom.HasValue) salary.EffectiveFrom = dto.EffectiveFrom.Value;

            salary.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(salary);

            // Update payroll for this employee
            await UpdatePayrollForEmployee(updated.EmployeeId);

            return _mapper.Map<SalaryDTO>(updated);
        }

        public async Task<IEnumerable<SalaryDTO>> GetAllAsync()
        {
            var salaries = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SalaryDTO>>(salaries);
        }

        public async Task<SalaryDTO?> GetByIdAsync(int id)
        {
            var salary = await _repository.GetByIdAsync(id);
            return salary == null ? null : _mapper.Map<SalaryDTO>(salary);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salary = await _repository.GetByIdAsync(id);
            if (salary == null) return false;

            var deleted = await _repository.DeleteAsync(id);

            // Update payroll for this employee
            await UpdatePayrollForEmployee(salary.EmployeeId);

            return deleted;
        }

        /// <summary>
        /// Updates all payrolls for an employee based on the latest salary
        /// </summary>
        private async Task UpdatePayrollForEmployee(int employeeId)
        {
            var payrolls = await _payrollRepository.GetByEmployeeIdAsync(employeeId);
            if (payrolls == null || !payrolls.Any()) return;

            var salaries = await _repository.GetByEmployeeIdAsync(employeeId);
            var latestSalary = salaries.OrderByDescending(s => s.EffectiveFrom).First();

            foreach (var payroll in payrolls)
            {
                payroll.TotalSalary = latestSalary.BasicSalary + latestSalary.Bonus - latestSalary.Deduction;
                payroll.NetSalary = payroll.TotalSalary - payroll.Tax;
                payroll.UpdatedAt = DateTime.UtcNow;

                await _payrollRepository.UpdateAsync(payroll);
            }
        }
    }
}
