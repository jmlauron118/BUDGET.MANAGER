using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetAllRoles(int status);
        Task<List<RoleModel>> GetRoleById(int roleId);
        Task<List<RoleModel>> AddRole(RoleModel role);
        Task<List<RoleModel>> ModifyRole(RoleModel role);
    }
}
