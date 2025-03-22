using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BUDGET.MANAGER.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IModuleService _moduleService;

        /**
         * Constructor
         * @param userService - The user service
         */
        public UserManagerController(IUserService userService,
                                     IModuleService moduleService)
        {
            _userService = userService;
            _moduleService = moduleService;
        }

        /**
         * Display the user manager page
         */
        public IActionResult Index()
        {
            ViewData["Title"] = "User Manager";

            return View();
        }

        /**
         * Get all users
         */
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

        /**
         * Get user by ID
         * @param userId - The ID of the user to get
         */
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

        /**
         * Add a new user
         * @param user - The user to add
         */
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

        /**
         * Modify an existing user
         * @param user - The user to modify
         */
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

        [HttpPost]
        public async Task<IActionResult> GetAllModules()
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {
                var modules = await _moduleService.GetAllModules();
                if (modules.Count > 0)
                {
                    _response.Data = modules;
                    _response.Status = 1;
                    _response.Message = "Modules loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No modules found.";
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
        public async Task<IActionResult> GetModuleById(int moduleId)
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {
                var module = await _moduleService.GetModuleById(moduleId);

                if (module.Count > 0)
                {
                    _response.Data = module;
                    _response.Status = 1;
                    _response.Message = "Module loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Module does not exist.";
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
        public async Task<IActionResult> AddModule(ModuleModel module)
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {
                //Temporary values since there are no authentication yet
                module.CreatedBy = 1;
                module.DateCreated = DateTime.Now;
                module.UpdatedBy = 1;
                module.DateUpdated = DateTime.Now;

                var modules = await _moduleService.AddModule(module);

                if (modules.Count > 0)
                {
                    _response.Data = modules;
                    _response.Status = 1;
                    _response.Message = "Module added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No modules found.";
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
        public async Task<IActionResult> ModifyModule(ModuleModel module)
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {
                //Temporary values since there are no authentication yet
                module.UpdatedBy = 1;
                module.DateUpdated = DateTime.Now;

                var modules = await _moduleService.ModifyModule(module);

                if (modules.Count > 0)
                {
                    _response.Data = modules;
                    _response.Status = 1;
                    _response.Message = "Module modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No modules found.";
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
        public async Task<IActionResult> DeleteModule(int moduleId)
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {
                var modules = await _moduleService.DeleteModule(moduleId);
                if (modules.Count > 0)
                {
                    _response.Data = modules;
                    _response.Status = 1;
                    _response.Message = "Module deleted successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No modules found.";
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
