using HRMS.Backend.Data;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Backend.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly HRMSDbContext _context;

        public PayrollRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<Payroll> AddAsync(Payroll payroll)
        {
            _context.Payrolls.Add(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

        public async Task<IEnumerable<Payroll>> GetAllAsync()
        {
            return await _context.Payrolls
                                 .Include(p => p.Employee)
                                 .ToListAsync();
        }

        public async Task<Payroll?> GetByIdAsync(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }

        public async Task<IEnumerable<Payroll>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Payrolls
                                 .Where(p => p.EmployeeId == employeeId)
                                 .ToListAsync();
        }

        public async Task<Payroll?> GetByEmployeeAndMonthAsync(int employeeId, DateTime payrollMonth)
        {
            return await _context.Payrolls
                                 .FirstOrDefaultAsync(p => p.EmployeeId == employeeId 
                                                           && p.PayrollMonth.Month == payrollMonth.Month
                                                           && p.PayrollMonth.Year == payrollMonth.Year);
        }

        public async Task<Payroll> UpdateAsync(Payroll payroll)
        {
            _context.Payrolls.Update(payroll);
            await _context.SaveChangesAsync();
            return payroll;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            if (payroll == null) return false;

            _context.Payrolls.Remove(payroll);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
