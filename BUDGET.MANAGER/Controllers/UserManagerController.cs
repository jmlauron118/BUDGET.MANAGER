using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.ViewModels.UserManager;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpPost]
        public async Task<IActionResult> GetAllUsers()
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                var users = await _userService.GetAllUsers();

                if (users.Count > 0)
                {
                    _response.Data = users;
                    _response.Status = 1;
                    _response.Message = "Users loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No users found.";
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
        public async Task<IActionResult> GetUserById(int userId)
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                var user = await _userService.GetUserById(userId);

                if (user.Count > 0)
                {
                    _response.Data = user;
                    _response.Status = 1;
                    _response.Message = "User loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "User does not exist.";
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
        public async Task<IActionResult> AddUser(UserModel user)
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                //Temporary values since there are no authentication yet
                user.CreatedBy = 1;
                user.DateCreated = DateTime.Now;
                user.UpdatedBy = 1;
                user.DateUpdated = DateTime.Now;

                var users = await _userService.AddUser(user);

                if (users.Count > 0)
                {
                    _response.Data = users;
                    _response.Status = 1;
                    _response.Message = "User added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No users found.";
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
        public async Task<IActionResult> ModifyUser(UserModel user)
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                //Temporary values since there are no authentication yet
                user.UpdatedBy = 1;
                user.DateUpdated = DateTime.Now;

                var users = await _userService.ModifyUser(user);

                if (users.Count > 0)
                {
                    _response.Data = users;
                    _response.Status = 1;
                    _response.Message = "User modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No users found."; ;
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
