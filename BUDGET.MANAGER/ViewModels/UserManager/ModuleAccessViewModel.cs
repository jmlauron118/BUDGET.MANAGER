using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class ModuleAccessViewModel
    {
        [Required(ErrorMessage = "Module action is required")]
        public string ModuleAccessModuleAction { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public string ModuleAccessUserRole { get; set; }
    }
}
