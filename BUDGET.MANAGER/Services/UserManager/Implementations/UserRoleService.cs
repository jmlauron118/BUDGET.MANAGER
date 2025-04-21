using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDbContext _context;

        public UserRoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetAllUserRoles()
        {
            try
            {
                var userRoles = from userRole in _context.UserRoles
                                join user in _context.Users on userRole.UserId equals user.UserId
                                join role in _context.Roles on userRole.RoleId equals role.RoleId
                                where user.IsActive == 1 && role.IsActive == 1
                                select new
                                {
                                    userRole.UserRoleId,
                                    userRole.UserId,
                                    UserName = user.Username,
                                    userRole.RoleId,
                                    RoleName = role.Role
                                };

                return await userRoles.ToListAsync<object>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> GetUserRoleById(int userRoleId)
        {
            try
            {
                return await _context.UserRoles.Where(x => x.UserRoleId == userRoleId).ToListAsync<object>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> AddUserRole(UserRoleModel userRole)
        {
            try
            {
                var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);
                if (existingUserRole != null)
                {
                    return new List<object> { "User role already exists." };
                }
                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();
                return new List<object> { "User role added successfully." };
            }
            catch
            {
                throw;
            }
        }
    }
}
