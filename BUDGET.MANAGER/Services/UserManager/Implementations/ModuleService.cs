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

        public async Task<List<ModuleModel>> GetAllModules(int status)
        {
            try
            {
                return await _context.Modules.Where(module => status == 2 || module.IsActive == status).OrderBy(x => x.SortNo).ThenBy(x => x.ModuleName).ToListAsync();
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
                var existingModule = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleName == module.ModuleName || m.ModulePage == module.ModulePage);

                if (existingModule != null)
                {
                    throw new Exception("Module already exists.");
                }

                _context.Modules.Add(module);
                await _context.SaveChangesAsync();

                return await GetAllModules(2);
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
                var existingModule = await _context.Modules.FirstOrDefaultAsync(m => (m.ModuleName == module.ModuleName || m.ModulePage == module.ModulePage) && m.ModuleId != module.ModuleId);

                if (existingModule != null)
                {
                    throw new Exception("Module already exists.");
                }

                _context.Entry(module).Property(u => u.ModuleName).IsModified = true;
                _context.Entry(module).Property(u => u.Description).IsModified = true;
                _context.Entry(module).Property(u => u.ModulePage).IsModified = true;
                _context.Entry(module).Property(u => u.IsActive).IsModified = true;
                _context.Entry(module).Property(u => u.Icon).IsModified = true;
                _context.Entry(module).Property(u => u.SortNo).IsModified = true;
                _context.Entry(module).Property(u => u.UpdatedBy).IsModified = true;
                _context.Entry(module).Property(u => u.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllModules(2);
            }
            catch
            {
                throw;
            }
        }
    }
}
