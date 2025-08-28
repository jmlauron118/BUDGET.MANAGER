using FINANCE.TRACKER.Models.UserManager;

namespace FINANCE.TRACKER.Services.UserManager.Interfaces
{
    public interface IModuleActionService
    {
        Task<List<object>> GetAllModuleActions();
        Task<List<ModuleActionModel>> GetModuleActionById(int moduleActionId);
        Task<List<object>> AddModuleAction(ModuleActionModel moduleAction);
        Task<List<object>> ModifyModuleAction(ModuleActionModel moduleAction);
        Task<List<object>> RemoveModuleAction(int moduleActionId);
    }
}
