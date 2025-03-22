using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
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
                return await _context.Users.Where(x => x.UserId == userId).ToListAsync();
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
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return await _context.Users.ToListAsync();
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
                _context.Entry(user).Property(u => u.Firstname).IsModified = true;
                _context.Entry(user).Property(u => u.Lastname).IsModified = true;
                _context.Entry(user).Property(u => u.Gender).IsModified = true;
                _context.Entry(user).Property(u => u.Username).IsModified = true;
                _context.Entry(user).Property(u => u.IsActive).IsModified = true;
                _context.Entry(user).Property(u => u.UpdatedBy).IsModified = true;
                _context.Entry(user).Property(u => u.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();
                return await _context.Users.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
