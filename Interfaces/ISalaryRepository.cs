using HRMS.Backend.Models;

namespace HRMS.Backend.Interfaces
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<Salary>> GetAllAsync();
        Task<Salary?> GetByIdAsync(int id);
        Task<IEnumerable<Salary>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Salary> AddAsync(Salary salary);
        Task<Salary> UpdateAsync(Salary salary);
        Task<bool> DeleteAsync(int id);
    }
}
