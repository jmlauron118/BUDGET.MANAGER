using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class UserRoleViewModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserRoleUserName { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        public string UserRoleRoleName { get; set; }
    }
}
