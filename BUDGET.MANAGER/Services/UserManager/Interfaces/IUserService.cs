using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers(int status);
        Task<List<UserModel>> GetUserById(int userId);
        Task<List<UserModel>> AddUser(UserModel user);
        Task<List<UserModel>> ModifyUser(UserModel user);
        Task<List<UserModel>> GetLogginUser(string username, string password);
    }
}
