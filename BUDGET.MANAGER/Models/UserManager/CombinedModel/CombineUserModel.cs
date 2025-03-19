using BUDGET.MANAGER.ViewModels.UserManager;

namespace BUDGET.MANAGER.Models.UserManager.CombinedModel
{
    public class CombineUserModel
    {
        public IEnumerable<UserModel>? Users { get; set; }
        public UserViewModel? UserValidation { get; set; }
    }
}
