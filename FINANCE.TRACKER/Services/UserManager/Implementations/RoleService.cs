using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models.UserManager;
using FINANCE.TRACKER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER.Services.UserManager.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleModel>> GetAllRoles(int status)
        {
            try
            {
                return await _context.Roles.Where(r => status == 2 || r.IsActive == status).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RoleModel>> GetRoleById(int roleId)
        {
            try
            {
                return await _context.Roles.Where(r => r.RoleId == roleId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RoleModel>> AddRole(RoleModel role)
        {
            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Role == role.Role);

                if (existingRole != null)
                {
                    throw new Exception("Role already exists.");
                }

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return await GetAllRoles(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<RoleModel>> ModifyRole(RoleModel role)
        {
            try
            {
                var existingRole = await _context.Roles.FirstOrDefaultAsync(r => r.Role == role.Role && r.RoleId != role.RoleId);

                if (existingRole != null)
                {
                    throw new Exception("Role already exists.");
                }

                _context.Entry(role).Property(r => r.Role).IsModified = true;
                _context.Entry(role).Property(r => r.Description).IsModified = true;
                _context.Entry(role).Property(r => r.IsActive).IsModified = true;
                _context.Entry(role).Property(r => r.UpdatedBy).IsModified = true;
                _context.Entry(role).Property(r => r.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllRoles(2);
            }
            catch
            {
                throw;
            }
        }
    }
}
