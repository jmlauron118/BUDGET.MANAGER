using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.ViewModels.UserManager
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Role description is required.")]
        public string RoleDescription { get; set; }
    }
}
