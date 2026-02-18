using HRMS.Backend.DTOs;
using HRMS.Backend.Interfaces;
using HRMS.Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> RegisterAsync(RegisterDTO dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                IsActive = true
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            return await _repository.AddAsync(user);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _repository.GetByEmailAsync(email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<User> UpdateUserAsync(int userId, UpdateUserDTO dto)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            user.Username = dto.FullName ?? user.Username;
            user.Phone = dto.Phone ?? user.Phone;
            user.Address = dto.Address ?? user.Address;
            return await _repository.UpdateAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
