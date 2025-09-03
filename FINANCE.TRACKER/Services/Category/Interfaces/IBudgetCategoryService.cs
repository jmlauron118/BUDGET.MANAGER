using FINANCE.TRACKER.Models.Category;

namespace FINANCE.TRACKER.Services.Category.Interfaces
{
    public interface IBudgetCategoryService
    {
        Task<List<BudgetCategoryModel>> GetAllCategories(int status);
        Task<List<BudgetCategoryModel>> GetCategoryById(int budgetCategoryId);
        Task<List<BudgetCategoryModel>> AddBudgetCategory(BudgetCategoryModel budgetCategory);
        Task<List<BudgetCategoryModel>> ModifyBudgetCategory(BudgetCategoryModel budgetCategory);
    }
}
