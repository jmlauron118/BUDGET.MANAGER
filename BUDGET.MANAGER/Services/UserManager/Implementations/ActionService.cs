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
                var existingAction = await _context.Actions.FirstOrDefaultAsync(a => a.ActionName == action.ActionName);

                if (existingAction != null)
                {
                    throw new Exception("Action already exist.");
                }

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
            try
            {
                var existingAction = await _context.Actions.FirstOrDefaultAsync(a => a.ActionName == action.ActionName && a.ActionId != action.ActionId);

                if (existingAction != null)
                {
                    throw new Exception("Action already exist.");
                }

                _context.Entry(action).Property(a => a.ActionName).IsModified = true;
                _context.Entry(action).Property(a => a.Description).IsModified = true;
                _context.Entry(action).Property(a => a.IsActive).IsModified = true;
                _context.Entry(action).Property(a => a.UpdatedBy).IsModified = true;
                _context.Entry(action).Property(a => a.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllActions(2);
            }
            catch
            {
                throw;
            }
        }
    }
}
