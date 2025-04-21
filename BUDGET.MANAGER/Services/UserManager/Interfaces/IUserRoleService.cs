using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.UserManager.Interfaces
{
    public interface IUserRoleService
    {
        Task<List<object>> GetAllUserRoles();
        Task<List<object>> GetUserRoleById(int userRoleId);
        Task<List<object>> AddUserRole(UserRoleModel userRole);
        Task<List<object>> ModifyUserRole(UserRoleModel userRole);
        Task<List<object>> RemoveUserRole(int userRoleId);
    }
}
