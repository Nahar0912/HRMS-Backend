using HRMS.Backend.DTOs;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllAsync(); 
    Task<EmployeeDTO?> GetByIdAsync(int id);
    Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO dto);
    Task<EmployeeDTO?> UpdateAsync(int id, EmployeeUpdateDTO dto);
    Task<bool> DeleteAsync(int id);
}
