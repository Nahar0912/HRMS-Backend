using HRMS.Backend.DTOs;

public interface IEmployeeService
{
    Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO dto);
    Task<EmployeeDTO?> UpdateAsync(int id, EmployeeUpdateDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<EmployeeDTO?> GetByIdAsync(int id);
    Task<IEnumerable<EmployeeDTO>> GetAllAsync(); // remove search parameters
}
