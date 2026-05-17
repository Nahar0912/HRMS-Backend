using HRMS.Backend.Models;

namespace HRMS.Backend.Interfaces
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<Payroll>> GetAllAsync();
        Task<Payroll?> GetByIdAsync(int id);
        Task<IEnumerable<Payroll>> GetByEmployeeIdAsync(int employeeId);
        Task<Payroll> AddAsync(Payroll payroll);
        Task<Payroll> UpdateAsync(Payroll payroll);
        Task<bool> DeleteAsync(int id);
        Task<Payroll?> GetByEmployeeAndMonthAsync(int employeeId, DateTime payrollMonth);
    }
}
