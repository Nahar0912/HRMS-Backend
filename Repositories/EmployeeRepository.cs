using HRMS.Backend.Models;
using HRMS.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using HRMS.Backend.Data;

namespace HRMS.Backend.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRMSDbContext _context;
        public EmployeeRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return false;

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
