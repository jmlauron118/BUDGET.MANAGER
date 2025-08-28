using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models.UserManager;
using FINANCE.TRACKER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER.Services.UserManager.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsers(int status)
        {
            try
            {
                return await _context.Users.Where(u => status == 2 || u.IsActive == status).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModel>> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.Where(u => u.UserId == userId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModel>> AddUser(UserModel user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

                if (existingUser != null)
                {
                    throw new Exception("Username already exists.");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return await GetAllUsers(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModel>> ModifyUser(UserModel user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.UserId != user.UserId);

                if (existingUser != null)
                {
                    throw new Exception("Username already exists.");
                }

                _context.Entry(user).Property(u => u.Firstname).IsModified = true;
                _context.Entry(user).Property(u => u.Lastname).IsModified = true;
                _context.Entry(user).Property(u => u.Gender).IsModified = true;
                _context.Entry(user).Property(u => u.Username).IsModified = true;
                _context.Entry(user).Property(u => u.IsActive).IsModified = true;
                _context.Entry(user).Property(u => u.UpdatedBy).IsModified = true;
                _context.Entry(user).Property(u => u.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllUsers(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModel>> GetLogginUser(string username, string password)
        {
            try
            {
                return await _context.Users.Where(u => u.Username == username && u.Password == password && u.IsActive == 1).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
