using BUDGET.MANAGER.Models.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace BUDGET.MANAGER.Models
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

            if (!string.IsNullOrEmpty(encryptedData))
            {
                string decrypted = _helper.Decrypt(encryptedData);

                var userData = JsonSerializer.Deserialize<UserDataModel>(decrypted);

                if (userData?.Username != null) // Ensure userData and Modules are not null
                {
                    httpContext.Items["UserId"] = Convert.ToInt32(userData.UserId);
                    httpContext.Items["Username"] = userData.Username;
                    httpContext.Items["UserModules"] = userData.Modules;
                }
            }

            base.OnResultExecuting(context);
        }
    }
}