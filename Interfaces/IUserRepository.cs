using HRMS.Backend.Models;

namespace HRMS.Backend.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);       // nullable
        Task<User?> GetByEmailAsync(string email); // nullable
    }
}
