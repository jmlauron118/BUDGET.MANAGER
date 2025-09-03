using FINANCE.TRACKER.Services.Category.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINANCE.TRACKER.Controllers
{
    public class BudgetManagerController : Controller
    {
        public BudgetManagerController(IBudgetCategoryService budgetCategoryService)
        {
            
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Budget Manager";

            return View();
        }
    }
}
