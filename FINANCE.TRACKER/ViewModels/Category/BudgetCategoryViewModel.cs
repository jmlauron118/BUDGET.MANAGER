using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.ViewModels.Category
{
    public class BudgetCategoryViewModel
    {
        [Required(ErrorMessage = "Budget category name is required")]
        public string BudgetCategoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string BudgetCategoryDescription { get; set; }
    }
}
