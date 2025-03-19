using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Models.UserManager.CombinedModel;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.ViewModels.UserManager;
using Microsoft.AspNetCore.Mvc;

namespace BUDGET.MANAGER.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly IUserService _userService;

        public UserManagerController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "User Manager";

            return View();
        }

        public async Task<IActionResult> LoadUsers()
        {
            var _response = new ResponseModel<List<UserModel>>();

            _response = await _userService.GetAllUsers();

            if (_response.Status == 2)
            {
                ModelState.AddModelError(string.Empty, _response.Message);
            }

            var data = new CombineUserModel
            {
                Users = _response.Data,
                UserValidation = new UserViewModel()
            };

            return PartialView("PartialViews/_Users", data);
        }

        public async Task<IActionResult> AddUser(UserModel user)
        {
            var _response = new ResponseModel<UserModel>();

            if (ModelState.IsValid)
            {
                _response = await _userService.AddUser(user);

                if (_response.Status == 2)
                {
                    ModelState.AddModelError(string.Empty, _response.Message);
                }
            }

            var data = new CombineUserModel
            {
                Users = (IEnumerable<UserModel>)_response.Data,
                UserValidation = new UserViewModel()
            };

            return PartialView("PartialViews/_Users", _response.Data);
        }
    }
}
