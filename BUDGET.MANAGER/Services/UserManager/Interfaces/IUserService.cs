using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;

namespace BUDGET.MANAGER.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel<List<UserModel>>> GetAllUsers();
        Task<ResponseModel<UserModel>> GetUserById(int userId);
        Task<ResponseModel<UserModel>> AddUser(UserModel user);
        Task<ResponseModel<UserModel>> UpdateUser(UserModel user);
        Task<ResponseModel<UserModel>> DeleteUser(int userId);
    }
}
