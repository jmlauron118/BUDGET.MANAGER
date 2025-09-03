using FINANCE.TRACKER.Models;
using FINANCE.TRACKER.Models.UserManager;
using FINANCE.TRACKER.Services.UserManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FINANCE.TRACKER.Controllers
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
        // The user role service
        private readonly IUserRoleService _userRoleService;
        // The module access service
        private readonly IModuleAccessService _moduleAccessService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Helper? _helper;

        public UserManagerController(
            IUserService userService,
            IRoleService roleService,
            IModuleService moduleService,
            IActionService actionService,
            IModuleActionService moduleActionService,
            IUserRoleService userRoleService,
            IModuleAccessService moduleAccessService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _moduleService = moduleService;
            _actionService = actionService;
            _moduleActionService = moduleActionService;
            _userRoleService = userRoleService;
            _moduleAccessService = moduleAccessService;
            _httpContextAccessor = httpContextAccessor;
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
            _helper = new Helper(_httpContextAccessor);

            try
            {
                int userId = _helper.GetUserId();

                user.CreatedBy = userId;
                user.DateCreated = DateTime.Now;
                user.UpdatedBy = userId;
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
            _helper = new Helper(_httpContextAccessor);

            try
            {
                user.UpdatedBy = _helper.GetUserId();
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

        /**
         * Get all user roles
         */
        [HttpPost]
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

        /**
         * Get role by ID
         *
         * @param roleId - The ID of the role to get
         */
        [HttpPost]
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

        /**
         * Add a new role
         *
         * @param roleModel - The role to add
         */
        [HttpPost]
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

        /**
         * Modify an existing role
         *
         * @param roleModel - The role to modify
         */
        [HttpPost]
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

        /**
         * Get all module actions
         */
        [HttpPost]
        public async Task<IActionResult> GetAllModuleActions()
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

        /**
         * Get module action by ID
         *
         * @param moduleActionId - The ID of the module action to get
         */
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

        /**
         * Add a new module action
         *
         * @param moduleAction - The module action to add
         */
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

        /**
         * Modify an existing module action
         *
         * @param moduleAction - The module action to modify
         */
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

        /**
         * Remove a module action
         *
         * @param moduleActionId - The ID of the module action to remove
         */
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

        [HttpPost]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                var userRoles = await _userRoleService.GetAllUserRoles();

                if (userRoles.Count > 0)
                {
                    _response.Data = userRoles;
                    _response.Status = 1;
                    _response.Message = "User Roles loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No User Roles found.";
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
        public async Task<IActionResult> GetUserRoleById(int userRoleId)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                var userRole = await _userRoleService.GetUserRoleById(userRoleId);

                if (userRole.Count > 0)
                {
                    _response.Data = userRole;
                    _response.Status = 1;
                    _response.Message = "User Role loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "User Role does not exist.";
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
        public async Task<IActionResult> AddUserRole(UserRoleModel userRole)
        {
            var _response = new ResponseModel<List<object>>();
            try
            {
                //Temporary values since there are no authentication yet
                userRole.CreatedBy = 1;
                userRole.DateCreated = DateTime.Now;
                userRole.UpdatedBy = 1;
                userRole.DateUpdated = DateTime.Now;

                var userRoles = await _userRoleService.AddUserRole(userRole);

                if (userRoles.Count > 0)
                {
                    _response.Data = userRoles;
                    _response.Status = 1;
                    _response.Message = "User Role added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No User Roles found.";
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
        public async Task<IActionResult> ModifyUserRole(UserRoleModel userRole)
        {
            var _response = new ResponseModel<List<object>>();
            try
            {
                //Temporary values since there are no authentication yet
                userRole.UpdatedBy = 1;
                userRole.DateUpdated = DateTime.Now;

                var userRoles = await _userRoleService.ModifyUserRole(userRole);

                if (userRoles.Count > 0)
                {
                    _response.Data = userRoles;
                    _response.Status = 1;
                    _response.Message = "User Role modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No User Roles found.";
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
        public async Task<IActionResult> RemoveUserRole(int userRoleId)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                _response.Data = await _userRoleService.RemoveUserRole(userRoleId);
                _response.Status = 1;
                _response.Message = "User Role deleted successfully.";
            }
            catch (Exception ex)
            {
                _response.Status = 2;
                _response.Message = ex.Message;
            }

            return Json(_response);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllModuleAccess()
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                var moduleAccess = await _moduleAccessService.GetAllModuleAccess();

                if (moduleAccess.Count > 0)
                {
                    _response.Data = moduleAccess;
                    _response.Status = 1;
                    _response.Message = "Module Access loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Access found.";
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
        public async Task<IActionResult> GetModuleAccessById(int moduleAccessId)
        {
            var _response = new ResponseModel<List<ModuleAccessModel>>();

            try
            {
                var moduleAccess = await _moduleAccessService.GetModuleAccessById(moduleAccessId);

                if (moduleAccess.Count > 0)
                {
                    _response.Data = moduleAccess;
                    _response.Status = 1;
                    _response.Message = "Module Access loaded successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "Module Access does not exist.";
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
        public async Task<IActionResult> AddModuleAccess(ModuleAccessModel moduleAccess)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                //Temporary values since there are no authentication yet
                moduleAccess.CreatedBy = 1;
                moduleAccess.DateCreated = DateTime.Now;
                moduleAccess.UpdatedBy = 1;
                moduleAccess.DateUpdated = DateTime.Now;

                var moduleAccesses = await _moduleAccessService.AddModuleAccess(moduleAccess);

                if (moduleAccesses.Count > 0)
                {
                    _response.Data = moduleAccesses;
                    _response.Status = 1;
                    _response.Message = "Module Access added successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Access found.";
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
        public async Task<IActionResult> ModifyModuleAccess(ModuleAccessModel moduleAccess)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                //Temporary values since there are no authentication yet
                moduleAccess.UpdatedBy = 1;
                moduleAccess.DateUpdated = DateTime.Now;

                var moduleAccesses = await _moduleAccessService.ModifyModuleAccess(moduleAccess);

                if (moduleAccesses.Count > 0)
                {
                    _response.Data = moduleAccesses;
                    _response.Status = 1;
                    _response.Message = "Module Access modified successfully.";
                }
                else
                {
                    _response.Status = 0;
                    _response.Message = "No Module Access found.";
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
        public async Task<IActionResult> RemoveModuleAccess(int moduleAccessId)
        {
            var _response = new ResponseModel<List<object>>();

            try
            {
                _response.Data = await _moduleAccessService.RemoveModuleAccess(moduleAccessId);
                _response.Status = 1;
                _response.Message = "Module Access deleted successfully.";
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
