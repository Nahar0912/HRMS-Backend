using HRMS.Backend.DTOs;

namespace HRMS.Backend.Interfaces
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollDTO>> GetAllAsync();
        Task<PayrollDTO?> GetByIdAsync(int id);
        Task<PayrollDTO> CreateAsync(PayrollCreateDTO dto);
        Task<PayrollDTO?> UpdateAsync(int id, PayrollUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PayrollDTO>> GenerateMonthlyPayrollsAsync(DateTime month);
    }
}
