using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Role description is required.")]
        public string RoleDescription { get; set; }
    }
}
