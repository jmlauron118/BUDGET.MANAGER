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
                _context.Entry(action).Property(u => u.ActionName).IsModified = true;
                _context.Entry(action).Property(u => u.Description).IsModified = true;
                _context.Entry(action).Property(u => u.IsActive).IsModified = true;
                _context.Entry(action).Property(u => u.UpdatedBy).IsModified = true;
                _context.Entry(action).Property(u => u.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();
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
