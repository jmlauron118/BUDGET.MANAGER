using BUDGET.MANAGER.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace BUDGET.MANAGER.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICompositeViewEngine _viewEngine;

        public UserManagerController(AppDbContext context, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _viewEngine = viewEngine;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            var userTableHtml = await RenderPartialViewToString("./PartialViews/_Users", users);

            return View("Index", userTableHtml); // Pass the user table as the default content
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                return View();
            }
            var userTableHtml = await RenderPartialViewToString("./PartialViews/_Users", users);

            return View("Index", userTableHtml);
        }

        private async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            ViewData["Title"] = "User Manager";

            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
