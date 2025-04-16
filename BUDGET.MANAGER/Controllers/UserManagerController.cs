using BUDGET.MANAGER.Models;
using BUDGET.MANAGER.Models.UserManager;
using BUDGET.MANAGER.Services.Interfaces;
using BUDGET.MANAGER.Services.UserManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BUDGET.MANAGER.Controllers
{
    /**
     * The user manager controller
     */
    public class UserManagerController : Controller
    {
        // The user service
        private readonly IUserService _userService;
        // The role service
        private readonly IRoleService _roleService;
        // The module service
        private readonly IModuleService _moduleService;
        // The action service
        private readonly IActionService _actionService;
        // The module action service
        private readonly IModuleActionService _moduleActionService;

        /**
         * Constructor
         * @param userService - The user service
         * @param moduleService - The module service
         * @param actionService - The action service
         * @param moduleActionService - The module action service
         */
        public UserManagerController(IUserService userService,
                                     IRoleService roleService,
                                     IModuleService moduleService,
                                     IActionService actionService,
                                     IModuleActionService moduleActionService)
        {
            _userService = userService;
            _roleService = roleService;
            _moduleService = moduleService;
            _actionService = actionService;
            _moduleActionService = moduleActionService;
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
        public async Task<IActionResult> GetAllUsers(int status)
        {
            var _response = new ResponseModel<List<UserModel>>();

            try
            {
                var users = await _userService.GetAllUsers(status);

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

        public async Task<IActionResult> GetAllRoles(int status)
        {
            var _response = new ResponseModel<List<RoleModel>>();

            try
            {
                var roles = await _roleService.GetAllRoles(status);
                if (roles.Count > 0)
                {
                    _response.Data = roles;
                    _response.Status = 1;
                    _response.Message = "Roles loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No roles found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var _response = new ResponseModel<List<RoleModel>>();

            try
            {
                var role = await _roleService.GetRoleById(roleId);

                if (role.Count > 0)
                {
                    _response.Data = role;
                    _response.Status = 1;
                    _response.Message = "Role loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Role does not exist.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        public async Task<IActionResult> AddRole(RoleModel roleModel)
        {
            var _response = new ResponseModel<List<RoleModel>>();

            try
            {
                //Temporary values since there are no authentication yet
                roleModel.CreatedBy = 1;
                roleModel.DateCreated = DateTime.Now;
                roleModel.UpdatedBy = 1;
                roleModel.DateUpdated = DateTime.Now;

                var roles = await _roleService.AddRole(roleModel);

                if (roles.Count > 0)
                {
                    _response.Data = roles;
                    _response.Status = 1;
                    _response.Message = "Role added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No roles found.";
                }
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        public async Task<IActionResult> ModifyRole(RoleModel roleModel)
        {
            var _response = new ResponseModel<List<RoleModel>>();

            try
            {
                //Temporary values since there are no authentication yet
                roleModel.UpdatedBy = 1;
                roleModel.DateUpdated = DateTime.Now;

                var roles = await _roleService.ModifyRole(roleModel);

                if (roles.Count > 0)
                {
                    _response.Data = roles;
                    _response.Status = 1;
                    _response.Message = "Role modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No roles found.";
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
        public async Task<IActionResult> GetAllModules(int status)
        {
            var _response = new ResponseModel<List<ModuleModel>>();

            try
            {
                var modules = await _moduleService.GetAllModules(status);

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
         * Get all actions
         */
        [HttpPost]
        public async Task<IActionResult> GetAllActions(int status)
        {
            var _response = new ResponseModel<List<ActionModel>>();
            try
            {
                var actions = await _actionService.GetAllActions(status);

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

        /**
         * Get action by ID
         *
         * @param actionId - The ID of the action to get
         */
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

        /**
         * Add a new action
         *
         * @param action - The action to add
         */
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

        /**
         * Modify an existing action
         *
         * @param action - The action to modify
         */
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
        public async Task<IActionResult> GetAllModuleActions(int status)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                var moduleActions = await _moduleActionService.GetAllModuleActions();

                if (moduleActions.Count > 0)
                {
                    _response.Data = moduleActions;
                    _response.Status = 1;
                    _response.Message = "Module Actions loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Actions found.";
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
        public async Task<IActionResult> GetModuleActionById(int moduleActionId)
        {
            var _response = new ResponseModel<List<ModuleActionModel>>();

            try
            {
                var moduleAction = await _moduleActionService.GetModuleActionById(moduleActionId);

                if (moduleAction.Count > 0)
                {
                    _response.Data = moduleAction;
                    _response.Status = 1;
                    _response.Message = "Module Action loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Module Action does not exist.";
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
        public async Task<IActionResult> AddModuleAction(ModuleActionModel moduleAction)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                //Temporary values since there are no authentication yet
                moduleAction.CreatedBy = 1;
                moduleAction.DateCreated = DateTime.Now;
                moduleAction.UpdatedBy = 1;
                moduleAction.DateUpdated = DateTime.Now;

                var moduleActions = await _moduleActionService.AddModuleAction(moduleAction);

                if (moduleActions.Count > 0)
                {
                    _response.Data = moduleActions;
                    _response.Status = 1;
                    _response.Message = "Module Action added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Actions found.";
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
        public async Task<IActionResult> ModifyModuleAction(ModuleActionModel moduleAction)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                //Temporary values since there are no authentication yet
                moduleAction.UpdatedBy = 1;
                moduleAction.DateUpdated = DateTime.Now;

                var moduleActions = await _moduleActionService.ModifyModuleAction(moduleAction);

                if (moduleActions.Count > 0)
                {
                    _response.Data = moduleActions;
                    _response.Status = 1;
                    _response.Message = "Module Action modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Actions found.";
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
        public async Task<IActionResult> RemoveModuleAction(int moduleActionId)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                _response.Data = await _moduleActionService.RemoveModuleAction(moduleActionId);
                _response.Status = 1;
                _response.Message = "Module Action deleted successfully.";
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
