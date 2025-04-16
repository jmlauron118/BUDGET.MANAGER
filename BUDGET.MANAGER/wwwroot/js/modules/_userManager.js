// @ts-nocheck
document.addEventListener("DOMContentLoaded", function () {
    const userManager = new UserManager();
    userManager.InitUserManager();

    const user = new User();
    user.GetAllUsers(2);
    user.InitUser();
});

/**
 * User Manager class
 */
function UserManager() {

    /**
     * Initialize the user manager
     */
    this.InitUserManager = function () {
        const submenu = $(".list-group-item");

        $("[type='checkbox']").bootstrapSwitch();

        submenu.off().on("click", function (e) {
            e.preventDefault();
            const target = $(e.target);

            if (target.hasClass("list-group-item")) {
                submenu.removeClass("active");
                target.addClass("active");
            }

            switch (target[0].id) {
                case "btnUsers":
                    const user = new User();
                    user.GetAllUsers(2);
                    user.InitUser();
                case "btnRoles":
                    const role = new Role();
                    role.GetAllRoles(2);
                    role.InitRole();
                case "btnModules":
                    const module = new Module();
                    module.GetAllModules(2);
                    module.InitModule();
                case "btnActions":
                    const action = new Action();
                    action.GetAllActions(2);
                    action.InitAction();
                case "btnModuleActions":
                    const moduleAction = new ModuleAction();
                    moduleAction.GetAllModuleActions(2);
                    moduleAction.InitModuleAction();
            }
        });
    }
}

/**
 * User class
 */
function User() {
    const _user = this;

    /**
     * Initialize the user
     */
    this.InitUser = function () {
        $("#btnSubmitUser").data("action", "add");
        $("#btnSubmitUser").off().on("click", function () {
            var form = $("#userForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this user?`, function (e) {
                var isActive = $("#chkUserStatus").prop("checked") === true ? 1 : 0;
                var userData = {
                    Firstname: $("#Firstname").val(),
                    Lastname: $("#Lastname").val(),
                    Username: $("#Username").val(),
                    Gender: $("#Gender").val(),
                    IsActive: isActive
                };

                if (action == "update") {
                    userData.UserId = $("#btnSubmitUser").data("id");;

                    _user.ModifyUser(userData);
                }
                else {
                    userData.Password = $("#Password").val();
                    _user.AddUser(userData);
                }
            }).setHeader(`${CapitalizeWords(action)} user`);
        });

        $("#modalUserDetails").off().on("hidden.bs.modal", function () {
            $("#userModalTitle").text("New User");
            $("#btnSubmitUser").html('<i aria-hidden="true" class="fa fa-plus"></i> Add User');
            $("#btnSubmitUser").data("action", "add");
            $("#btnSubmitUser").removeAttr("data-id");
            $("#Password").prop("disabled", false);
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnUserPageToggle").off().on("click", function () {
            $("#btnClearUserForm").trigger("click");
        });
    }

    /**
     * Get all users
     */
    this.GetAllUsers = function (status) {
        $.post("/UserManager/GetAllUsers", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _user.PopulateUser(response.data);
            }
        });
    }

    /**
     * Get user by id
     * @param {any} userId
     * @returns
     */
    this.GetUserById = function (userId) {
        return new Promise((resolve, reject) => {
            $.post("/UserManager/GetUserById", { userId: userId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    /**
     * Populate user
     * @param {any} data
     */
    this.PopulateUser = function (data) {
        $("#user-table tbody").empty();

        if (data != null) {
            data.forEach(user => {
                const row = `
                        <tr>
                            <td>
                                <span>${user.firstname} ${user.lastname}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-user active" data-user-id="${user.userId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                </div>
                            </td>
                            <td>${user.gender}</td>
                            <td>${user.username}</td>
                            <td><div class="user-status badge badge-pill ${user.isActive == 1 ? "badge-success" : "badge-danger"}">${user.isActive == 1 ? "Active" : "Inactive"}</div></td>
                        </tr>
                    `;

                $("#user-table tbody").append(row);
            });
        }
        else {
            $("#user-table tbody").append(`<tr class="text-center">
                        <td colspan="4">No user data found.</td>
                    </tr>`);
        }

        $(".edit-user").on("click", function () {
            _user.GetUserById($(this).data("user-id")).then(userData => {
                var user = userData[0];

                $("#modalUserDetails").modal("show");
                $("#userModalTitle").text("Update User");
                $("#btnSubmitUser").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update User');
                $("#btnSubmitUser").data("action", "update");
                $("#btnSubmitUser").data("id", $(this).data("user-id"));

                $("#Firstname").val(user.firstname);
                $("#Lastname").val(user.lastname);
                $("#Username").val(user.username);
                $("#Password").prop("disabled", true);
                $("#Gender").val(user.gender);
                $("#chkUserStatus").bootstrapSwitch("state", user.isActive == 1 ? true : false);
            }).catch((err) => {
                Notif(err, "danger");
            });
        });
    };

    /**
     * Add user
     * @param {any} user
     */
    this.AddUser = function (user) {
        $.post("/UserManager/AddUser", { user: user }, function (response) {
            if (response.status === 2) {
                debugger;
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _user.PopulateUser(response.data);
                $("#modalUserDetails").modal("hide");
            }
        });
    }

    /**
     * Modify user
     * @param {any} user
     */
    this.ModifyUser = function (user) {
        $.post("/UserManager/ModifyUser", { user: user }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _user.PopulateUser(response.data);
                $("#modalUserDetails").modal("hide");
            }
        });
    }
}

function Role() {
    const _role = this;

    this.InitRole = function () {
        $("#btnSubmitRole").data("action", "add");
        $("#btnSubmitRole").off().on("click", function () {
            var form = $("#roleForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this role?`, function (e) {
                var isActive = $("#chkRoleStatus").prop("checked") === true ? 1 : 0;
                var roleData = {
                    Role: $("#Role").val(),
                    Description: $("#RoleDescription").val(),
                    IsActive: isActive
                };

                if (action == "update") {
                    roleData.roleId = $("#btnSubmitRole").data("id");;

                    _role.ModifyRole(roleData);
                }
                else {
                    _role.AddRole(roleData);
                }
            }).setHeader(`${CapitalizeWords(action)} role`);
        });

        $("#modalRoleDetails").off().on("hidden.bs.modal", function () {
            $("#roleModalTitle").text("New Role");
            $("#btnSubmitRole").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Role');
            $("#btnSubmitRole").data("action", "add");
            $("#btnSubmitRole").removeAttr("data-id");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnRolePageToggle").off().on("click", function () {
            $("#btnClearRoleForm").trigger("click");
        });
    }
    this.GetAllRoles = function (status) {
        $.post("/UserManager/GetAllRoles", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _role.PopulateRole(response.data);
            }
        });
    }

    this.GetRoleById = function (roleId) {
        return new Promise((resolve, reject) => {
            $.post("/UserManager/GetRoleById", { roleId: roleId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    this.PopulateRole = function (data) {
        $("#role-table tbody").empty();

        if (data != null) {
            data.forEach(roleData => {
                const row = `
                        <tr>
                            <td>
                                <span>${roleData.role}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-role active" data-role-id="${roleData.roleId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                </div>
                            </td>
                            <td>${roleData.description}</td>
                            <td><div class="role-status badge badge-pill ${roleData.isActive == 1 ? "badge-success" : "badge-danger"}">${roleData.isActive == 1 ? "Active" : "Inactive"}</div></td>
                        </tr>
                    `;

                $("#role-table tbody").append(row);
            });
        }
        else {
            $("#role-table tbody").append(`<tr class="text-center">
                        <td colspan="4">No role data found.</td>
                    </tr>`);
        }

        $(".edit-role").on("click", function () {
            _role.GetRoleById($(this).data("role-id")).then(roleData => {
                var role = roleData[0];

                $("#modalRoleDetails").modal("show");
                $("#roleModalTitle").text("Update Role");
                $("#btnSubmitRole").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Role');
                $("#btnSubmitRole").data("action", "update");
                $("#btnSubmitRole").data("id", $(this).data("role-id"));

                $("#Role").val(role.role);
                $("#RoleDescription").val(role.description);
                $("#chkRoleStatus").bootstrapSwitch("state", role.isActive == 1 ? true : false);
            }).catch((err) => {
                Notif(err, "danger");
            });
        });
    }

    this.AddRole = function (roleModel) {
        $.post("/UserManager/AddRole", { roleModel: roleModel }, function(response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _role.PopulateRole(response.data);
                Notif(response.message, "success");
                $("#modalRoleDetails").modal("hide");
            }
        });
    }

    this.ModifyRole = function (roleModel) {
        $.post("/UserManager/ModifyRole", { roleModel: roleModel }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _role.PopulateRole(response.data);
                Notif(response.message, "success");
                $("#modalRoleDetails").modal("hide");
            }
        });
    }
}

function Module() {
    const _module = this;

    this.InitModule = function () {
        $("#btnSubmitModule").data("action", "add");
        $("#btnSubmitModule").off().on("click", function () {
            var form = $("#moduleForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this module?`, function (e) {
                var isActive = $("#chkModuleStatus").prop("checked") === true ? 1 : 0;
                var moduleData = {
                    ModuleName: $("#ModuleName").val(),
                    ModulePage: $("#ModulePage").val(),
                    Description: $("#ModuleDescription").val(),
                    Icon: $("#Icon").val(),
                    SortNo: $("#txtSortNo").val(),
                    IsActive: isActive
                };

                if (action == "update") {
                    moduleData.ModuleId = $("#btnSubmitModule").data("id");

                    _module.ModifyModule(moduleData);
                }
                else {
                    _module.AddModule(moduleData);
                }

                $("#modalModuleDetails").modal("hide");
            }).setHeader(`${CapitalizeWords(action)} module`);
        });

        $("#modalModuleDetails").off().on("hidden.bs.modal", function () {
            $("#moduleModalTitle").text("New Module");
            $("#btnSubmitModule").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Module');
            $("#btnSubmitModule").data("action", "add");
            $("#btnSubmitModule").removeAttr("data-id");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnModulePageToggle").off().on("click", function () {
            $("#btnClearModuleForm").trigger("click");
        });
    }

    this.GetAllModules = function (status) {
        $.post("/UserManager/GetAllModules", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _module.PopulateModule(response.data);
            }
        });
    }

    this.GetModuleById = function (moduleId) {
        return new Promise((resolve, reject) => {
            $.post("/UserManager/GetModuleById", { moduleId: moduleId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    this.PopulateModule = function (data) {
        $("#module-table tbody").empty();

        if (data != null) {
            data.forEach(module => {
                const row = `
                        <tr>
                            <td>
                                <span>${module.moduleName}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-module active" data-module-id="${module.moduleId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                    <a class="remove-module active text-red" data-module-id="${module.moduleId}" href="#"><i aria-hidden="true" class="fa fa-trash"></i> Remove</a>
                                </div>
                            </td>
                            <td>${module.modulePage}</td>
                            <td>${module.description}</td>
                            <td><i class="${module.icon}"></i> | ${module.icon}</td>
                            <td>${module.sortNo}</td>
                            <td><div class="module-status badge badge-pill ${module.isActive == 1 ? "badge-success" : "badge-danger"}">${module.isActive == 1 ? "Active" : "Inactive"}</div></td>
                        </tr>
                    `;

                $("#module-table tbody").append(row);
            });
        }
        else {
            $("#module-table tbody").append(`<tr class="text-center">
                        <td colspan="6">No module data found.</td>
                    </tr>`);
        }

        $(".edit-module").on("click", function () {
            _module.GetModuleById($(this).data("module-id")).then((moduleData) => {
                var module = moduleData[0];

                $("#modalModuleDetails").modal("show");
                $("#moduleModalTitle").text("Update Module");
                $("#btnSubmitModule").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Module');
                $("#btnSubmitModule").data("action", "update");
                $("#btnSubmitModule").data("id", $(this).data("module-id"));

                $("#ModuleName").val(module.moduleName);
                $("#ModulePage").val(module.modulePage);
                $("#ModuleDescription").val(module.description);
                $("#Icon").val(module.icon);
                $("#txtSortNo").val(module.sortNo);
                $("#chkModuleStatus").bootstrapSwitch("state", module.isActive == 1 ? true : false);
            }).catch((err) => {
                Notif(`PopulateModule: ${err}`, "danger");
            });
        });

        $(".remove-module").on("click", function () {
            var moduleId = $(this).data("module-id");

            alertify.confirm("Are you sure you want to remove this module?", function (e) {
                _module.RemoveModule(moduleId);
            }).setHeader("Remove Module");
        });
    };

    this.AddModule = function (module) {
        $.post("/UserManager/AddModule", { module: module }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _module.PopulateModule(response.data);
            }
        });
    }

    this.ModifyModule = function (module) {
        $.post("/UserManager/ModifyModule", { module: module }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _module.PopulateModule(response.data);
            }
        });
    }

    this.RemoveModule = function (moduleId) {
        $.post("/UserManager/RemoveModule", { moduleId: moduleId }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _module.PopulateModule(response.data);
            }
        });
    }
}

function Action() {
    const _action = this;

    this.InitAction = function () {
        $("#btnSubmitAction").data("action", "add");
        $("#btnSubmitAction").off().on("click", function () {
            var form = $("#actionForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this action?`, function (e) {
                var isActive = $("#chkActionStatus").prop("checked") === true ? 1 : 0;

                var actionData = {
                    ActionName: $("#ActionName").val(),
                    Description: $("#ActionDescription").val(),
                    IsActive: isActive
                };

                if (action == "update") {
                    actionData.ActionId = $("#btnSubmitAction").data("id");

                    _action.ModifyAction(actionData);
                }
                else {
                    _action.AddAction(actionData);
                }

                $("#modalActionDetails").modal("hide");
            }).setHeader(`${CapitalizeWords(action)} action`);
        });

        $("#modalActionDetails").off().on("hidden.bs.modal", function () {
            $("#actionModalTitle").text("New Action");
            $("#btnSubmitAction").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Action');
            $("#btnSubmitAction").data("action", "add");
            $("#btnSubmitAction").removeAttr("data-id");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnActionPageToggle").off().on("click", function () {
            $("#btnClearActionForm").trigger("click");
        });
    }

    this.GetAllActions = function (status) {
        $.post("/UserManager/GetAllActions", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _action.PopulateAction(response.data);
            }
        });
    }

    this.GetActionById = function (actionId) {
        return new Promise((resolve, reject) => {
            $.post("/UserManager/GetActionById", { actionId: actionId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    this.PopulateAction = function (data) {
        $("#action-table tbody").empty();

        if (data != null) {
            data.forEach(action => {
                const row = `
                        <tr>
                            <td>
                                <span>${action.actionName}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-action active" data-action-id="${action.actionId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                </div>
                            </td>
                            <td>${action.description}</td>
                            <td><div class="action-status badge badge-pill ${action.isActive == 1 ? "badge-success" : "badge-danger"}">${action.isActive == 1 ? "Active" : "Inactive"}</div></td>
                        </tr>
                    `;

                $("#action-table tbody").append(row);
            });
        }
        else {
            $("#action-table tbody").append(`<tr class="text-center">
                        <td colspan="6">No action data found.</td>
                    </tr>`);
        }

        $(".edit-action").on("click", function () {
            _action.GetActionById($(this).data("action-id")).then((actionData) => {
                var action = actionData[0];

                $("#modalActionDetails").modal("show");
                $("#actionModalTitle").text("Update Action");
                $("#btnSubmitAction").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Action');
                $("#btnSubmitAction").data("action", "update");
                $("#btnSubmitAction").data("id", $(this).data("action-id"));

                $("#ActionName").val(action.actionName);
                $("#ActionDescription").val(action.description);
                $("#chkActionStatus").bootstrapSwitch("state", action.isActive == 1 ? true : false);
            }).catch((err) => {
                Notif(`PopulateAction: ${err}`, "danger");
            });
        });
    }

    this.AddAction = function (action) {
        $.post("/UserManager/AddAction", { action: action }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _action.PopulateAction(response.data);
            }
        });
    }

    this.ModifyAction = function (action) {
        $.post("/UserManager/ModifyAction", { action: action }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _action.PopulateAction(response.data);
            }
        });
    }
}

function ModuleAction() {
    const _moduleAction = this;

    this.InitModuleAction = function () {
        _moduleAction.LoadModules();
        _moduleAction.LoadAction();

        $("#btnSubmitModuleAction").data("action", "add");
        $("#btnSubmitModuleAction").off().on("click", function () {
            var form = $("#moduleActionForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this module action?`, function (e) {
                var moduleActionData = {
                    ModuleId: $("#ModuleActionModuleName").val(),
                    ActionId: $("#ModuleActionActionName").val()
                };

                if (action == "update") {
                    moduleActionData.ModuleActionId = $("#btnSubmitModuleAction").data("id");

                    _moduleAction.ModifyModuleAction(moduleActionData);
                }
                else {
                    _moduleAction.AddModuleAction(moduleActionData);
                }

                $("#modalModuleActionDetails").modal("hide");
            }).setHeader(`${CapitalizeWords(action)} module action`);
        });

        $("#modalModuleActionDetails").off().on("hidden.bs.modal", function () {
            $("#moduleActionModalTitle").text("New Module Action");
            $("#btnSubmitModuleAction").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Module Action');
            $("#btnSubmitModuleAction").data("module-action", "add");
            $("#btnSubmitModuleAction").removeAttr("data-id");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnModuleActionPageToggle").off().on("click", function () {
            $("#btnClearModuleActionForm").trigger("click");
        });
    }

    this.GetAllModuleActions = function (status) {
        $.post("/UserManager/GetAllModuleActions", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _moduleAction.PopulateModuleAction(response.data);
            }
        });
    }

    this.GetModuleActionById = function (moduleActionId) {
        return new Promise((resolve, reject) => {
            $.post("/UserManager/GetModuleActionById", { moduleActionId: moduleActionId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    this.PopulateModuleAction = function (data) {
        $("#module-action-table tbody").empty();

        if (data != null) {
            data.forEach(moduleAction => {
                const row = `
                        <tr>
                            <td>
                                <span>${moduleAction.moduleName}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-module-action active" data-module-action-id="${moduleAction.moduleActionId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                </div>
                            </td>
                            <td>${moduleAction.moduleDescription}</td>
                            <td>${moduleAction.actionName}</td>
                            <td>${moduleAction.actionDescription}</td>
                        </tr>
                    `;

                $("#module-action-table tbody").append(row);
            });
        }
        else {
            $("#module-action-table tbody").append(`<tr class="text-center">
                        <td colspan="6">No module action data found.</td>
                    </tr>`);
        }

        $(".edit-module-action").on("click", function () {
            _moduleAction.GetModuleActionById($(this).data("module-action-id")).then((moduleActionData) => {
                var moduleAction = moduleActionData[0];

                $("#modalModuleActionDetails").modal("show");
                $("#moduleActionModalTitle").text("Update Module Action");
                $("#btnSubmitModuleAction").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Module Action');
                $("#btnSubmitModuleAction").data("action", "update");
                $("#btnSubmitModuleAction").data("id", $(this).data("module-action-id"));

                $("#ModuleActionModuleName").val(moduleAction.moduleId);
                $("#ModuleActionActionName").val(moduleAction.actionId);
            }).catch((err) => {
                Notif(`PopulateModuleAction: ${err}`, "danger");
            });
        });
    }

    this.LoadModules = function () {
        $.post("/UserManager/GetAllModules", { status: 1 }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                $("#ModuleActionModuleName").empty();
                $("#ModuleActionModuleName").append(`<option value="">Select module</option>`);
                response.data.forEach(module => {
                    $("#ModuleActionModuleName").append(`<option value="${module.moduleId}">${module.moduleName}</option>`);
                });
            }
        });
    }

    this.LoadAction = function () {
        $.post("/UserManager/GetAllActions", { status: 1 }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                $("#ModuleActionActionName").empty();
                $("#ModuleActionActionName").append(`<option value="">Select action</option>`);
                response.data.forEach(action => {
                    $("#ModuleActionActionName").append(`<option value="${action.actionId}">${action.actionName}</option>`);
                });
            }
        });
    }

    this.AddModuleAction = function (moduleAction) {
        $.post("/UserManager/AddModuleAction", { moduleAction: moduleAction }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _moduleAction.PopulateModuleAction(response.data);
            }
        });
    }

    this.ModifyModuleAction = function (moduleAction) {
        $.post("/UserManager/ModifyModuleAction", { moduleAction: moduleAction }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _moduleAction.PopulateModuleAction(response.data);
            }
        });
    }
}