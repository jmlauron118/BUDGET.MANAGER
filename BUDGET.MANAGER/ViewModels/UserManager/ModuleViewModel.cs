using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class ModuleViewModel
    {
        [Required(ErrorMessage = "Module ID is required.")]
        public string ModuleName { get; set; }

        [Required(ErrorMessage = "Module Description is required.")]
        public string ModuleDescription { get; set; }

        [Required(ErrorMessage = "Module Page is required.")]
        public string ModulePage { get; set; }

        [Required(ErrorMessage = "Module Icon is required.")]
        public string Icon { get; set; }
    }
}
