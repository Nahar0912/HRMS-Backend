using HRMS.Backend.Models;

namespace HRMS.Backend.Interfaces
{
    public interface IPayrollRepository
    {
        Task<Payroll> AddAsync(Payroll payroll);
        Task<IEnumerable<Payroll>> GetAllAsync();
        Task<Payroll?> GetByIdAsync(int id);
        Task<IEnumerable<Payroll>> GetByEmployeeIdAsync(int employeeId);
        Task<Payroll?> GetByEmployeeAndMonthAsync(int employeeId, DateTime payrollMonth);

        Task<Payroll> UpdateAsync(Payroll payroll);
        Task<bool> DeleteAsync(int id);
    }
}
