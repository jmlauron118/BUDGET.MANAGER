using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.ViewModels.UserManager
{
    public class ActionViewModel
    {
        [Required(ErrorMessage = "Action Name is required")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string ActionDescription { get; set; }
    }
}
