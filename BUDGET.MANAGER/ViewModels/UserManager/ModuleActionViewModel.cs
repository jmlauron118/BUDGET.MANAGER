using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class ModuleActionViewModel
    {
        [Required(ErrorMessage = "Module name is required")]
        public string ModuleActionModuleName { get; set; }

        [Required(ErrorMessage = "Action name is required")]
        public string ModuleActionActionName { get; set; }
    }
}
