using FINANCE.TRACKER.Models.Login;
using FINANCE.TRACKER.Models.UserManager;

namespace FINANCE.TRACKER.Services.UserManager.Interfaces
{
    public interface IModuleAccessService
    {
        Task<List<object>> GetAllModuleAccess();
        Task<List<ModuleAccessModel>> GetModuleAccessById(int moduleAccessId);
        Task<List<object>> AddModuleAccess(ModuleAccessModel moduleAccess);
        Task<List<object>> ModifyModuleAccess(ModuleAccessModel moduleAccess);
        Task<List<object>> RemoveModuleAccess(int moduleAccessId);
        Task<List<UserModuleModel>> GetUserModules(int userId);
    }
}
