using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IModuleActionService
    {
        Task<List<object>> GetAllModuleActions();
        Task<List<ModuleActionModel>> GetModuleActionById(int moduleActionId);
        Task<List<ModuleActionModel>> AddModuleAction(ModuleActionModel moduleAction);
        Task<List<ModuleActionModel>> ModifyModuleAction(ModuleActionModel moduleAction);
        Task<List<ModuleActionModel>> RemoveModuleAction(int moduleActionId);
    }
}
