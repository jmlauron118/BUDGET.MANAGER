using System.ComponentModel.DataAnnotations;

namespace BUDGET.MANAGER.ViewModels.UserManager
{
    public class ActionViewModel
    {
        [Required(ErrorMessage = "Action Id is required")]
        public string? ActionName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
    }
}
