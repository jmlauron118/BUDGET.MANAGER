using FINANCE.TRACKER.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace FINANCE.TRACKER.Models
{
    public class ViewModuleFilter : ActionFilterAttribute
    {
        private readonly Helper _helper;

        public ViewModuleFilter(IHttpContextAccessor httpContextAccessor)
        {
            _helper = new Helper(httpContextAccessor);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var encryptedData = httpContext.Session.GetString("UserSession");
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var actionName = context.RouteData.Values["action"]?.ToString();

            if (controllerName == "Login" && actionName == "Index")
            {
                base.OnResultExecuting(context);
                return;
            }

            if (!string.IsNullOrEmpty(encryptedData))
            {
                string decrypted = _helper.Decrypt(encryptedData);

                var userData = JsonSerializer.Deserialize<UserDataModel>(decrypted);

                if (userData?.Username != null)
                {
                    httpContext.Items["UserId"] = Convert.ToInt32(userData.UserId);
                    httpContext.Items["Username"] = userData.Username;
                    httpContext.Items["UserModules"] = userData.Modules;
                }
            }
            else
            {
                var controller = (Controller)context.Controller;

                controller.TempData["ErrorMessage"] = "Your session has expired. Please log in again.";
                context.Result = controller.RedirectToAction("Index", "Login");

                return;
            }

            base.OnResultExecuting(context);
        }
    }
}