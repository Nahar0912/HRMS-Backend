using HRMS.Backend.Data;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;
using Microsoft.EntityFrameworkCore;


namespace HRMS.Backend.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly HRMSDbContext _context;
        public SalaryRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<Salary> AddAsync(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();
            return salary;
        }

        public async Task<IEnumerable<Salary>> GetAllAsync()
        {
            return await _context.Salaries.Include(s => s.Employee).ToListAsync();
        }

        public async Task<Salary?> GetByIdAsync(int id)
        {
            return await _context.Salaries.FindAsync(id);
        }

        public async Task<IEnumerable<Salary>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Salaries.Where(s => s.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Salary> UpdateAsync(Salary salary)
        {
            _context.Salaries.Update(salary);
            await _context.SaveChangesAsync();
            return salary;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null) return false;

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
