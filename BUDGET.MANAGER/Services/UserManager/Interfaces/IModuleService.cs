using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IModuleService
    {
        Task<List<ModuleModel>> GetAllModules();
        Task<List<ModuleModel>> GetModuleById(int moduleId);
        Task<List<ModuleModel>> AddModule(ModuleModel module);
        Task<List<ModuleModel>> ModifyModule(ModuleModel module);
        Task<List<ModuleModel>> DeleteModule(int moduleId);
    }
}
