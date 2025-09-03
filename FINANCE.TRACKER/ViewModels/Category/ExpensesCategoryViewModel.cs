using System.ComponentModel.DataAnnotations;

namespace FINANCE.TRACKER.ViewModels.Category
{
    public class ExpensesCategoryViewModel
    {
        [Required(ErrorMessage = "Expenses category name is required")]
        public string ExpensesCategoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string ExpensesCategoryDescription { get; set; }
    }
}
