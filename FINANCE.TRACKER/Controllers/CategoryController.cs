using FINANCE.TRACKER.Models;
using FINANCE.TRACKER.Models.Category;
using FINANCE.TRACKER.Services.Category.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINANCE.TRACKER.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBudgetCategoryService _budgetCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Helper? _helper;

        public CategoryController(IBudgetCategoryService budgetCategoryService, IHttpContextAccessor httpContextAccessor)
        {
            _budgetCategoryService = budgetCategoryService;
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
    }
}
