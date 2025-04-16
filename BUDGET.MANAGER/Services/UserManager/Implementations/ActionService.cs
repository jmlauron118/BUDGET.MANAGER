using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class ActionService : IActionService
    {
        private readonly AppDbContext _context;

        public ActionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActionModel>> GetAllActions(int status)
        {
            try
            {
                return await _context.Actions.Where(a => status == 2 || a.IsActive == status).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActionModel>> GetActionById(int actionId)
        {
            try
            {
                return await _context.Actions.Where(a => a.ActionId == actionId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActionModel>> AddAction(ActionModel action)
        {
            try
            {
                _context.Actions.Add(action);
                await _context.SaveChangesAsync();

                return await GetAllActions(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActionModel>> ModifyAction(ActionModel action)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var actionResp = await _context.Actions.FirstOrDefaultAsync(a => a.ActionId == action.ActionId);

                    if (actionResp != null)
                    {
                        _context.Actions.Remove(actionResp);
                        await _context.SaveChangesAsync();

                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Actions ON");

                        var updatedAction = new ActionModel
                        {
                            ActionId = action.ActionId,
                            ActionName = action.ActionName,
                            Description = action.Description,
                            IsActive = action.IsActive,
                            CreatedBy = actionResp.CreatedBy,
                            DateCreated = actionResp.DateCreated,
                            UpdatedBy = action.UpdatedBy,
                            DateUpdated = action.DateUpdated
                        };

                        _context.Actions.Add(updatedAction);
                        await _context.SaveChangesAsync();
                        await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Actions OFF");
                        await transaction.CommitAsync();
                    }

                    return await GetAllActions(2);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
