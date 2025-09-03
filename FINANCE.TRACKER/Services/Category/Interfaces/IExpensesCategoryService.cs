using FINANCE.TRACKER.Models.Category;

namespace FINANCE.TRACKER.Services.Category.Interfaces
{
    public interface IExpensesCategoryService
    {
        Task<List<ExpensesCategoryModel>> GetAllExpensesCategories(int status);
        Task<List<ExpensesCategoryModel>> GetExpensesCategoryById(int expensesCategoryId);
        Task<List<ExpensesCategoryModel>> AddExpensesCategory(ExpensesCategoryModel expensesCategory);
        Task<List<ExpensesCategoryModel>> ModifyExpensesCategory(ExpensesCategoryModel expensesCategory);
    }
}
