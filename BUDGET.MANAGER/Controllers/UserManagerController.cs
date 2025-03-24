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
        private readonly IActionService _actionService;

        /**
         * Constructor
         * @param userService - The user service
         */
        public UserManagerController(IUserService userService,
                                     IModuleService moduleService,
                                     IActionService actionService)
        {
            _userService = userService;
            _moduleService = moduleService;
            _actionService = actionService;
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

        /**
         * Get all modules
         *
         * @param moduleId - The ID of the module to get
         */
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

        /**
         * Get module by ID
         *
         * @param moduleId - The ID of the module to get
         */
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

        /**
         * Add a new module
         *
         * @param module - The module to add
         */
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

        /**
         * Modify an existing module
         *
         * @param module - The module to modify
         */
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

        /**
         * Delete a module
         *
         * @param moduleId - The ID of the module to delete
         */
        [HttpPost]
        public async Task<IActionResult> RemoveModule(int moduleId)
        {
            var _response = new ResponseModel<List<ModuleModel>>();
            try
            {                
                _response.Data = await _moduleService.RemoveModule(moduleId);
                _response.Status = 1;
                _response.Message = "Module deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }
            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllActions()
        {
            var _response = new ResponseModel<List<ActionModel>>();
            try
            {
                var actions = await _actionService.GetAllActions();

                if (actions.Count > 0)
                {
                    _response.Data = actions;
                    _response.Status = 1;
                    _response.Message = "Actions loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No actions found.";
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
        public async Task<IActionResult> GetActionById(int actionId)
        {
            var _response = new ResponseModel<List<ActionModel>>();

            try
            {
                var action = await _actionService.GetActionById(actionId);

                if (action.Count > 0)
                {
                    _response.Data = action;
                    _response.Status = 1;
                    _response.Message = "Action loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Action does not exist.";
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
        public async Task<IActionResult> AddAction(ActionModel action)
        {
            var _response = new ResponseModel<List<ActionModel>>();
            try
            {
                //Temporary values since there are no authentication yet
                action.CreatedBy = 1;
                action.DateCreated = DateTime.Now;
                action.UpdatedBy = 1;
                action.DateUpdated = DateTime.Now;

                var actions = await _actionService.AddAction(action);

                if (actions.Count > 0)
                {
                    _response.Data = actions;
                    _response.Status = 1;
                    _response.Message = "Action added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No actions found.";
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
        public async Task<IActionResult> ModifyAction(ActionModel action)
        {
            var _response = new ResponseModel<List<ActionModel>>();
            try
            {
                //Temporary values since there are no authentication yet
                action.UpdatedBy = 1;
                action.DateUpdated = DateTime.Now;

                var actions = await _actionService.ModifyAction(action);

                if (actions.Count > 0)
                {
                    _response.Data = actions;
                    _response.Status = 1;
                    _response.Message = "Action modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No actions found.";
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
        public async Task<IActionResult> RemoveAction(int actionId)
        {
            var _response = new ResponseModel<List<ActionModel>>();
            try
            {
                _response.Data = await _actionService.RemoveAction(actionId);
                _response.Status = 1;
                _response.Message = "Action deleted successfully.";
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
