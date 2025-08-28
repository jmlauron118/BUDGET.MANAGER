using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.Login;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BUDGET.MANAGER.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IModuleAccessService _moduleAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(
            IUserService userService,
            IModuleAccessService moduleAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _moduleAccessService = moduleAccessService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var encryptedSession = HttpContext.Session.GetString("UserSession");

            if (!string.IsNullOrEmpty(encryptedSession))
            {
                string decrypted = new Helper(_httpContextAccessor).Decrypt(encryptedSession);
                var userData = JsonSerializer.Deserialize<UserDataModel>(decrypted);

                if (userData?.Modules != null && userData.Modules.Count > 0)
                {
                    var module = userData.Modules[0];
                    return RedirectToAction("Index", module.ModuleName);
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserSession");

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> GetLogginUser(string username, string password)
        {
            var _response = new ResponseModel<object>();

            try
            {
                var user = await _userService.GetLogginUser(username, password);

                if (user != null && user.Count > 0)
                {
                    // Explicitly cast the userModules to the correct type
                    var userModules = await _moduleAccessService.GetUserModules(user[0].UserId);

                    var userDetails = new UserDataModel
                    {
                        UserId = user[0].UserId,
                        Username = user[0].Username,
                        Modules = userModules
                    };

                    string userData = JsonSerializer.Serialize(userDetails);
                    string encryptedData = new Helper(_httpContextAccessor).Encrypt(userData);

                    HttpContext.Session.SetString("UserSession", encryptedData);

                    var session = HttpContext.Session.GetString("UserSession");

                    if (!string.IsNullOrEmpty(session))
                    {
                        _response.Status = 1;
                        _response.Message = "Login Successfully!";
                        _response.Data = userModules[0];
                    }
                    else
                    {
                        _response.Status = 0;
                        _response.Message = "Failed to create session.";
                    }
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Invalid username or password.";
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
