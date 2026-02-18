using HRMS.Backend.Models;
using HRMS.Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using HRMS.Backend.Data;

namespace HRMS.Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HRMSDbContext _context;

        public UserRepository(HRMSDbContext context)
        {
            _context = context;
        }

        // Add new user
        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Delete user by Id
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Get user by email (nullable)
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Get user by Id (nullable)
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Update user
        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
