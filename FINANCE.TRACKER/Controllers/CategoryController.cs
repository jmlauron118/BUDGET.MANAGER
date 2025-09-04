using FINANCE.TRACKER.Models;
using FINANCE.TRACKER.Models.Category;
using FINANCE.TRACKER.Services.Category.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINANCE.TRACKER.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBudgetCategoryService _budgetCategoryService;
        private readonly IExpensesCategoryService _expensesCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Helper? _helper;

        public CategoryController
            (
                IBudgetCategoryService budgetCategoryService,
                IExpensesCategoryService expensesCategoryService,
                IHttpContextAccessor httpContextAccessor
            )
        {
            _budgetCategoryService = budgetCategoryService;
            _expensesCategoryService = expensesCategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Category";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetAllBudgetCategories(int status)
        {
            var _response = new ResponseModel<List<BudgetCategoryModel>>();

            try
            {
                var budgetCategories = await _budgetCategoryService.GetAllCategories(status);

                if (budgetCategories.Count > 0)
                {
                    _response.Data = budgetCategories;
                    _response.Status = 1;
                    _response.Message = "Budget categories loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No budget category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBudgetCategoryById(int budgetCategoryId)
        {
            var _response = new ResponseModel<List<BudgetCategoryModel>>();

            try
            {
                var budgetCategory = await _budgetCategoryService.GetCategoryById(budgetCategoryId);

                if (budgetCategory.Count > 0)
                {
                    _response.Data = budgetCategory;
                    _response.Status = 1;
                    _response.Message = "Budget category loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No budget category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> AddBudgetCategory(BudgetCategoryModel budgetCategory)
        {
            var _response = new ResponseModel<List<BudgetCategoryModel>>();
            _helper = new Helper(_httpContextAccessor);

            try
            {
                int userId = _helper.GetUserId();

                budgetCategory.CreatedBy = userId;
                budgetCategory.DateCreated = DateTime.Now;
                budgetCategory.UpdatedBy = userId;
                budgetCategory.DateUpdated = DateTime.Now;

                var budget = await _budgetCategoryService.AddBudgetCategory(budgetCategory);

                if (budget.Count > 0)
                {
                    _response.Data = budget;
                    _response.Status = 1;
                    _response.Message = "Budget category added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No budget category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyBudgetCategory(BudgetCategoryModel budgetCategory)
        {
            var _response = new ResponseModel<List<BudgetCategoryModel>>();
            _helper = new Helper(_httpContextAccessor);

            try
            {
                budgetCategory.UpdatedBy = _helper.GetUserId(); ;
                budgetCategory.DateUpdated = DateTime.Now;

                var budget = await _budgetCategoryService.ModifyBudgetCategory(budgetCategory);

                if (budget.Count > 0)
                {
                    _response.Data = budget;
                    _response.Status = 1;
                    _response.Message = "Budget category modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No budget category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllExpensesCategories(int status)
        {
            var _response = new ResponseModel<List<ExpensesCategoryModel>>();

            try
            {
                var expensesCategories = await _expensesCategoryService.GetAllExpensesCategories(status);

                if (expensesCategories.Count > 0)
                {
                    _response.Data = expensesCategories;
                    _response.Status = 1;
                    _response.Message = "Expenses categories loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No expenses category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> GetExpensesCategoryById(int expensesCategoryId)
        {
            var _response = new ResponseModel<List<ExpensesCategoryModel>>();

            try
            {
                var expensesCategory = await _expensesCategoryService.GetExpensesCategoryById(expensesCategoryId);

                if (expensesCategory.Count > 0)
                {
                    _response.Data = expensesCategory;
                    _response.Status = 1;
                    _response.Message = "Expenses category loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No expenses category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpensesCategory(ExpensesCategoryModel expensesCategory)
        {
            var _response = new ResponseModel<List<ExpensesCategoryModel>>();
            _helper = new Helper(_httpContextAccessor);

            try
            {
                int userId = _helper.GetUserId();

                expensesCategory.CreatedBy = userId;
                expensesCategory.DateCreated = DateTime.Now;
                expensesCategory.UpdatedBy = userId;
                expensesCategory.DateUpdated = DateTime.Now;

                var expenses = await _expensesCategoryService.AddExpensesCategory(expensesCategory);

                if (expenses.Count > 0)
                {
                    _response.Data = expenses;
                    _response.Status = 1;
                    _response.Message = "Expenses category added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No expenses category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyExpensesCategory(ExpensesCategoryModel expensesCategory)
        {
            var _response = new ResponseModel<List<ExpensesCategoryModel>>();
            _helper = new Helper(_httpContextAccessor);

            try
            {
                expensesCategory.UpdatedBy = _helper.GetUserId();
                expensesCategory.DateUpdated = DateTime.Now;

                var expenses = await _expensesCategoryService.ModifyExpensesCategory(expensesCategory);

                if (expenses.Count > 0)
                {
                    _response.Data = expenses;
                    _response.Status = 1;
                    _response.Message = "Expenses category modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No expenses category found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }
    }
}
