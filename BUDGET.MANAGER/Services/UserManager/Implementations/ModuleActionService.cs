using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class ModuleActionService : IModuleActionService
    {
        private readonly AppDbContext _context;

        public ModuleActionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<object>> GetAllModuleActions()
        {
            try
            {
                var moduleActions = from moduleAction in _context.ModuleActions
                                    join module in _context.Modules on moduleAction.ModuleId equals module.ModuleId
                                    join action in _context.Actions on moduleAction.ActionId equals action.ActionId
                                    where module.IsActive == 1 && action.IsActive == 1
                                    select new
                                    {
                                        moduleAction.ModuleActionId,
                                        moduleAction.ModuleId,
                                        module.ModuleName,
                                        ModuleDescription = module.Description,
                                        moduleAction.ActionId,
                                        action.ActionName,
                                        ActionDescription = action.Description
                                    };

                return await moduleActions.ToListAsync<object>();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleActionModel>> GetModuleActionById(int moduleActionId)
        {
            try
            {
                return await _context.ModuleActions.Where(x => x.ModuleActionId == moduleActionId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> AddModuleAction(ModuleActionModel moduleAction)
        {
            try
            {
                var existingModuleAction = await _context.ModuleActions.FirstOrDefaultAsync(ma => ma.ModuleId == moduleAction.ModuleId && ma.ActionId == moduleAction.ActionId);

                if (existingModuleAction != null)
                {
                    throw new Exception("Module Action already exists.");
                }

                _context.ModuleActions.Add(moduleAction);
                await _context.SaveChangesAsync();

                return await GetAllModuleActions();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> ModifyModuleAction(ModuleActionModel moduleAction)
        {
            try
            {
                var existingModuleAction = await _context.ModuleActions.FirstOrDefaultAsync(ma => ma.ModuleId == moduleAction.ModuleId && ma.ActionId == moduleAction.ActionId && ma.ModuleActionId != moduleAction.ModuleActionId);

                if (existingModuleAction != null)
                {
                    throw new Exception("Module Action already exists.");
                }

                _context.Entry(moduleAction).Property(ma => ma.ModuleId).IsModified = true;
                _context.Entry(moduleAction).Property(ma => ma.ActionId).IsModified = true;
                _context.Entry(moduleAction).Property(ma => ma.UpdatedBy).IsModified = true;
                _context.Entry(moduleAction).Property(ma => ma.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllModuleActions();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<object>> RemoveModuleAction(int moduleActionId)
        {
            try
            {
                var moduleActionResp = await _context.ModuleActions.FirstOrDefaultAsync(e => e.ModuleActionId == moduleActionId);

                if (moduleActionResp != null)
                {
                    var moduleAccess = await _context.ModuleAccess.FirstOrDefaultAsync(e => e.ModuleActionId == moduleActionId);

                    if (moduleAccess != null)
                    {
                        _context.ModuleAccess.Remove(moduleAccess);
                        await _context.SaveChangesAsync();
                    }

                    _context.ModuleActions.Remove(moduleActionResp);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Module Action not found");
                }

                return await GetAllModuleActions();
            }
            catch
            {
                throw;
            }
        }
    }
}
