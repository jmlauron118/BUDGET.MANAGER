using FINANCE.TRACKER.Models;
using FINANCE.TRACKER.Models.UserManager;

namespace FINANCE.TRACKER.Services.UserManager.Interfaces
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
