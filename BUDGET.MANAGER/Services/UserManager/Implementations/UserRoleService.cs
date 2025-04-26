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
                                    FullName = user.Lastname + ", " + user.Firstname,
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
                var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId);

                if (existingUserRole != null)
                {
                    throw new Exception("The role for the selected user has already been assigned.");
                }

                _context.UserRoles.Add(userRole);
                await _context.SaveChangesAsync();

                return await GetAllUserRoles();
            }
            catch

            {
                throw;
            }
        }

        public async Task<List<object>> ModifyUserRole(UserRoleModel userRole)
        {
            try
            {
                var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userRole.UserId && ur.UserRoleId != userRole.UserRoleId);

                if (existingUserRole != null)
                {
                    throw new Exception("The role for the selected user has already been assigned.");
                }

                _context.Entry(userRole).Property(ur => ur.UserId).IsModified = true;
                _context.Entry(userRole).Property(ur => ur.RoleId).IsModified = true;
                _context.Entry(userRole).Property(ur => ur.UpdatedBy).IsModified = true;
                _context.Entry(userRole).Property(ur => ur.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllUserRoles();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> RemoveUserRole(int userRoleId)
        {
            try
            { 
                var userRole = await _context.UserRoles.FindAsync(userRoleId);

                if (userRole != null)
                {
                    var moduleAccess = await _context.ModuleAccess.FindAsync(userRoleId);
                    if (moduleAccess != null)
                    {
                        _context.ModuleAccess.Remove(moduleAccess);
                        await _context.SaveChangesAsync();
                    }

                    _context.UserRoles.Remove(userRole);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User role not found.");
                }

                return await GetAllUserRoles();
            }
            catch
            {
                throw;
            }
        }
    }
}
