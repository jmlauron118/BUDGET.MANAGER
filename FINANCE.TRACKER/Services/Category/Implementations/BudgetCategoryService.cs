using FINANCE.TRACKER.Data;
using FINANCE.TRACKER.Models.Category;
using FINANCE.TRACKER.Services.Category.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FINANCE.TRACKER.Services.Category.Implementations
{
    public class BudgetCategoryService : IBudgetCategoryService
    {
        private readonly AppDbContext _context;

        public BudgetCategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BudgetCategoryModel>> GetAllCategories(int status)
        {
            try
            {
                return await _context.BudgetCategories.Where(c => status == 2 || c.IsActive == status).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<BudgetCategoryModel>> GetCategoryById(int budgetCategoryId)
        {
            try
            {
                return await _context.BudgetCategories.Where(c => c.BudgetCategoryId == budgetCategoryId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<BudgetCategoryModel>> AddBudgetCategory(BudgetCategoryModel budgetCategory)
        {
            try
            {
                var existingCategory = await _context.BudgetCategories.FirstOrDefaultAsync(c => c.BudgetCategoryName == budgetCategory.BudgetCategoryName);

                if (existingCategory != null)
                {
                    throw new Exception("Budget category already exist.");
                }

                _context.BudgetCategories.Add(budgetCategory);
                await _context.SaveChangesAsync();

                return await GetAllCategories(2);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<BudgetCategoryModel>> ModifyBudgetCategory(BudgetCategoryModel budgetCategory)
        {
            try
            {
                var existingCategory = await _context.BudgetCategories.FirstOrDefaultAsync(c => c.BudgetCategoryName == budgetCategory.BudgetCategoryName && c.BudgetCategoryId != budgetCategory.BudgetCategoryId);

                if (existingCategory != null)
                {
                    throw new Exception("Budget category already exists.");
                }

                _context.Entry(budgetCategory).Property(c => c.BudgetCategoryName).IsModified = true;
                _context.Entry(budgetCategory).Property(c => c.BudgetCategoryDescription).IsModified = true;
                _context.Entry(budgetCategory).Property(c => c.IsActive).IsModified = true;
                _context.Entry(budgetCategory).Property(c => c.UpdatedBy).IsModified = true;
                _context.Entry(budgetCategory).Property(c => c.DateUpdated).IsModified = true;

                await _context.SaveChangesAsync();

                return await GetAllCategories(2);
            }
            catch
            {
                throw;
            }
        }
    }
}
