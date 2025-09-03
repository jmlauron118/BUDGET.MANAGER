using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models.Category;
using FINANCE.TRACKER.Services.Category.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FINANCE.TRACKER.Services.Category.Implementations
{
    public class ExpensesCategoryService : IExpensesCategoryService
    {
        private readonly AppDbContext _context;

        public ExpensesCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExpensesCategoryModel>> GetAllExpensesCategories(int status)
        {
            try
            {
                return await _context.ExpenseCategories.Where(c => status == 2 || c.IsActive == status).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ExpensesCategoryModel>> GetExpensesCategoryById(int expensesCategoryId)
        {
            try
            {
                return await _context.ExpenseCategories.Where(c => c.ExpensesCategoryId == expensesCategoryId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ExpensesCategoryModel>> AddExpensesCategory(ExpensesCategoryModel expensesCategory)
        {
            try
            {
                var existingCategory = _context.ExpenseCategories.FirstOrDefault(c => c.ExpensesCategoryName == expensesCategory.ExpensesCategoryName);

                if (existingCategory != null)
                {
                    throw new Exception("Expenses category already exist.");
                }

                _context.ExpenseCategories.Add(expensesCategory);
                await _context.SaveChangesAsync();

                return await GetAllExpensesCategories(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<ExpensesCategoryModel>> ModifyExpensesCategory(ExpensesCategoryModel expensesCategory)
        {
            try
            {
                var existingCategory = _context.ExpenseCategories.FirstOrDefaultAsync(c => c.ExpensesCategoryName == expensesCategory.ExpensesCategoryName && c.ExpensesCategoryId != expensesCategory.ExpensesCategoryId);

                if (existingCategory != null)
                {
                    throw new Exception("Expenses category already exist.");
                }

                _context.Entry(expensesCategory).Property(c => c.ExpensesCategoryName).IsModified = true;
                _context.Entry(expensesCategory).Property(c => c.ExpensesCategoryDescription).IsModified = true;
                _context.Entry(expensesCategory).Property(c => c.IsActive).IsModified = true;
                _context.Entry(expensesCategory).Property(c => c.UpdatedBy).IsModified = true;
                _context.Entry(expensesCategory).Property(c => c.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllExpensesCategories(2);
            }
            catch
            {
                throw;
            }
        }
    }
}
