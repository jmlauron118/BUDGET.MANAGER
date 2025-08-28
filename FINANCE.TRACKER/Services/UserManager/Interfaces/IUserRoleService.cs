using FINANCE.TRACKER.Models.UserManager;

namespace FINANCE.TRACKER.Services.UserManager.Interfaces
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
