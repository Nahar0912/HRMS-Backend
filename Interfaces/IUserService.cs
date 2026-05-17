using HRMS.Backend.DTOs;
using HRMS.Backend.Models;

namespace HRMS.Backend.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDTO dto);
        Task<User?> LoginAsync(string email, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(int userId, UpdateUserDTO dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
