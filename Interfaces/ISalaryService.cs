using HRMS.Backend.DTOs;

namespace HRMS.Backend.Interfaces
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryDTO>> GetAllAsync();
        Task<SalaryDTO?> GetByIdAsync(int id);
        Task<SalaryDTO> CreateAsync(SalaryCreateDTO dto);
        Task<SalaryDTO?> UpdateAsync(int id, SalaryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
