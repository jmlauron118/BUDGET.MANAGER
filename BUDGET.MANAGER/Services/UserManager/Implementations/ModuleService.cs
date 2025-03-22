using BUDGET.MANAGER.Data;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Services.UserManager.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly AppDbContext _context;

        public ModuleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ModuleModel>> GetAllModules()
        {
            try
            {
                return await _context.Modules.OrderBy(x => x.SortNo).ThenBy(x => x.ModuleName).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleModel>> GetModuleById(int moduleId)
        {
            try
            {
                return await _context.Modules.Where(x => x.ModuleId == moduleId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleModel>> AddModule(ModuleModel module)
        {
            try
            {
                _context.Modules.Add(module);
                await _context.SaveChangesAsync();
                return await GetAllModules();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleModel>> ModifyModule(ModuleModel module)
        {
            try
            {
                _context.Entry(module).Property(u => u.ModuleName).IsModified = true;
                _context.Entry(module).Property(u => u.Description).IsModified = true;
                _context.Entry(module).Property(u => u.ModulePage).IsModified = true;
                _context.Entry(module).Property(u => u.IsActive).IsModified = true;
                _context.Entry(module).Property(u => u.Icon).IsModified = true;
                _context.Entry(module).Property(u => u.UpdatedBy).IsModified = true;
                _context.Entry(module).Property(u => u.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();
                return await GetAllModules();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ModuleModel>> DeleteModule(int moduleId)
        {
            try
            {
                var module = await _context.Modules.Where(x => x.ModuleId == moduleId).FirstOrDefaultAsync();

                if (module != null)
                {
                    _context.Modules.Remove(module);
                    await _context.SaveChangesAsync();
                }
                return await GetAllModules();
            }
            catch
            {
                throw;
            }
        }
    }
}
