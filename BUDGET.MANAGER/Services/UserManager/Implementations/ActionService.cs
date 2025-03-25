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

        public async Task<List<ActionModel>> GetAllActions()
        {
            try
            {
                return await _context.Actions.ToListAsync();
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
                return await _context.Actions.Where(x => x.ActionId == actionId).ToListAsync();
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
                return await _context.Actions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActionModel>> ModifyAction(ActionModel action)
        {
            try
            {
                var actionResp = await _context.Actions.FirstOrDefaultAsync(e => e.ActionId == action.ActionId);

                if (actionResp != null)
                {
                    _context.Entry(actionResp).State = EntityState.Detached;

                    actionResp.ActionName = action.ActionName;
                    actionResp.Description = action.Description;
                    actionResp.IsActive = action.IsActive;
                    actionResp.UpdatedBy = action.UpdatedBy;
                    actionResp.DateUpdated = action.DateUpdated;

                    //_context.Entry(actionResp).Property(u => u.Description).IsModified = true;
                    //_context.Entry(actionResp).Property(u => u.IsActive).IsModified = true;
                    //_context.Entry(actionResp).Property(u => u.UpdatedBy).IsModified = true;
                    //_context.Entry(actionResp).Property(u => u.DateUpdated).IsModified = true;

                    _context.Update(actionResp);
                    await _context.SaveChangesAsync();
                }

                return await _context.Actions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ActionModel>> RemoveAction(int actionId)
        {
            try
            {
                var action = await _context.Actions.FindAsync(actionId);

                if (action != null)
                {
                    _context.Actions.Remove(action);
                    await _context.SaveChangesAsync();
                }

                return await _context.Actions.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
