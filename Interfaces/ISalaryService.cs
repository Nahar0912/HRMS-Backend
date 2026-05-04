using HRMS.Backend.DTOs;

namespace HRMS.Backend.Interfaces
{
    public interface ISalaryService
    {
        Task<SalaryDTO> CreateAsync(SalaryCreateDTO dto);
        Task<IEnumerable<SalaryDTO>> GetAllAsync();
        Task<SalaryDTO?> GetByIdAsync(int id);
        Task<SalaryDTO?> UpdateAsync(int id, SalaryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
