using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models.Login;
using FINANCE.TRACKER.Models.UserManager;
using FINANCE.TRACKER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER.Services.UserManager.Implementations
{
    public class ModuleAccessService : IModuleAccessService
    {
        private readonly AppDbContext _context;

        public ModuleAccessService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetAllModuleAccess()
        {
            try
            {
                var moduleAccessList = from moduleAccess in _context.ModuleAccess
                                       join moduleAction in _context.ModuleActions on moduleAccess.ModuleActionId equals moduleAction.ModuleActionId
                                       join module in _context.Modules on moduleAction.ModuleId equals module.ModuleId
                                       join action in _context.Actions on moduleAction.ActionId equals action.ActionId
                                       join userRole in _context.UserRoles on moduleAccess.UserRoleId equals userRole.UserRoleId
                                       join user in _context.Users on userRole.UserId equals user.UserId
                                       join role in _context.Roles on userRole.RoleId equals role.RoleId
                                       where module.IsActive == 1 && action.IsActive == 1 && user.IsActive == 1 && role.IsActive == 1
                                       select new
                                       {
                                           moduleAccess.ModuleAccessId,
                                           moduleAccess.ModuleActionId,
                                           module.ModuleName,
                                           module.Icon,
                                           action.ActionName,
                                           moduleAccess.UserRoleId,
                                           user.Username,
                                           role.Role
                                       };

                return await moduleAccessList.ToListAsync<object>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleAccessModel>> GetModuleAccessById(int moduleAccessId)
        {
            try
            {
                return await _context.ModuleAccess.Where(x => x.ModuleAccessId == moduleAccessId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> AddModuleAccess(ModuleAccessModel moduleAccess)
        {
            try
            {
                var existingModuleAccess = await _context.ModuleAccess.FirstOrDefaultAsync(ma => ma.ModuleActionId == moduleAccess.ModuleActionId && ma.UserRoleId == moduleAccess.UserRoleId);

                if (existingModuleAccess != null)
                {
                    throw new Exception("Module Access already exists.");
                }

                _context.ModuleAccess.Add(moduleAccess);
                await _context.SaveChangesAsync();

                return await GetAllModuleAccess();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> ModifyModuleAccess(ModuleAccessModel moduleAccess)
        {
            try
            {
                var existingModuleAccess = await _context.ModuleAccess.FirstOrDefaultAsync(ma => ma.ModuleActionId == moduleAccess.ModuleActionId && ma.UserRoleId == moduleAccess.UserRoleId && ma.ModuleAccessId != moduleAccess.ModuleAccessId);

                if (existingModuleAccess != null)
                {
                    throw new Exception("Module Access already exists.");
                }

                _context.Entry(moduleAccess).Property(mac => mac.ModuleActionId).IsModified = true;
                _context.Entry(moduleAccess).Property(mac => mac.UserRoleId).IsModified = true;
                _context.Entry(moduleAccess).Property(mac => mac.UpdatedBy).IsModified = true;
                _context.Entry(moduleAccess).Property(mac => mac.DateUpdated).IsModified = true;
                await _context.SaveChangesAsync();

                return await GetAllModuleAccess();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> RemoveModuleAccess(int moduleAccessId)
        {
            try
            {
                var moduleAccess = await _context.ModuleAccess.FindAsync(moduleAccessId);

                if (moduleAccess == null)
                {
                    throw new Exception("Module Access not found.");
                }

                _context.ModuleAccess.Remove(moduleAccess);
                await _context.SaveChangesAsync();

                return await GetAllModuleAccess();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<UserModuleModel>> GetUserModules(int userId)
        {
            try
            {
                var userModules = from moduleAccess in _context.ModuleAccess
                                  join moduleAction in _context.ModuleActions on moduleAccess.ModuleActionId equals moduleAction.ModuleActionId
                                  join module in _context.Modules on moduleAction.ModuleId equals module.ModuleId
                                  join action in _context.Actions on moduleAction.ActionId equals action.ActionId
                                  join userRole in _context.UserRoles on moduleAccess.UserRoleId equals userRole.UserRoleId
                                  join user in _context.Users on userRole.UserId equals user.UserId
                                  join role in _context.Roles on userRole.RoleId equals role.RoleId
                                  where module.IsActive == 1 && action.IsActive == 1 && user.IsActive == 1 && role.IsActive == 1 && user.UserId == userId
                                  orderby module.SortNo
                                  select new UserModuleModel
                                  {
                                      ModuleId = module.ModuleId,
                                      ModuleName = module.ModuleName,
                                      ModulePage = module.ModulePage,
                                      Icon = module.Icon,
                                      ActionName = action.ActionName,
                                      SortNo = module.SortNo
                                  };

                return await userModules.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
