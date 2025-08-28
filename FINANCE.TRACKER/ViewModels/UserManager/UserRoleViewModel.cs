using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.ViewModels.UserManager
{
    public class UserRoleViewModel
    {
        [Required(ErrorMessage = "User name is required.")]
        public string UserRoleUserName { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        public string UserRoleRoleName { get; set; }
    }
}
