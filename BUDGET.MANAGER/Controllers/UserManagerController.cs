using Microsoft.AspNetCore.Mvc;

namespace BUDGET.MANAGER.Controllers
{
    public class UserManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
