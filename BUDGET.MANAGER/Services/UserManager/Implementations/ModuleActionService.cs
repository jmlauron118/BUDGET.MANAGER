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
                                        moduleAction,
                                        module.ModuleName,
                                        ModuleDescription = module.Description,
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

        public async Task<List<ModuleActionModel>> AddModuleAction(ModuleActionModel moduleAction)
        {
            try
            {
                _context.ModuleActions.Add(moduleAction);
                await _context.SaveChangesAsync();

                return await _context.ModuleActions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleActionModel>> ModifyModuleAction(ModuleActionModel moduleAction)
        {
            try
            {
                var moduleActionResp = await _context.ModuleActions.FirstOrDefaultAsync(e => e.ModuleActionId == moduleAction.ModuleActionId);

                if (moduleActionResp != null)
                {
                    moduleActionResp.ModuleId = moduleAction.ModuleId;
                    moduleActionResp.ActionId = moduleAction.ActionId;
                    moduleActionResp.UpdatedBy = moduleAction.UpdatedBy;
                    moduleActionResp.DateUpdated = DateTime.Now;

                    _context.ModuleActions.Update(moduleActionResp);
                    await _context.SaveChangesAsync();
                }

                return await _context.ModuleActions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleActionModel>> RemoveModuleAction(int moduleActionId)
        {
            try
            {
                var moduleActionResp = await _context.ModuleActions.FirstOrDefaultAsync(e => e.ModuleActionId == moduleActionId);

                if (moduleActionResp != null)
                {
                    _context.ModuleActions.Remove(moduleActionResp);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("ModuleAction not found");
                }

                return await _context.ModuleActions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
