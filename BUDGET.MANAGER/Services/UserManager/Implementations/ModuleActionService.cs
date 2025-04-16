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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var moduleActionResp = await _context.ModuleActions.FirstOrDefaultAsync(e => e.ModuleActionId == moduleAction.ModuleActionId);

                    if (moduleActionResp != null)
                    {
                        _context.ModuleActions.Remove(moduleActionResp);
                        await _context.SaveChangesAsync();

                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ModuleActions ON");

                        var updatedModuleAction = new ModuleActionModel{
                            ModuleActionId = moduleAction.ModuleActionId,
                            ModuleId = moduleAction.ModuleId,
                            ActionId = moduleAction.ActionId,
                            CreatedBy = moduleActionResp.CreatedBy,
                            DateCreated = moduleActionResp.DateCreated,
                            UpdatedBy = moduleAction.UpdatedBy,
                            DateUpdated = moduleAction.DateUpdated
                        };

                        _context.ModuleActions.Add(updatedModuleAction);
                        await _context.SaveChangesAsync();
                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT ModuleActions OFF");
                        await transaction.CommitAsync();
                    }

                    return await GetAllModuleActions();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<object>> RemoveModuleAction(int moduleActionId)
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

                return await GetAllModuleActions();
            }
            catch
            {
                throw;
            }
        }
    }
}
