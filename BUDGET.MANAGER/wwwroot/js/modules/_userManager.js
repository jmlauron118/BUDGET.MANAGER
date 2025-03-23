// @ts-nocheck
document.addEventListener("DOMContentLoaded", function () {
    const userManager = new UserManager();
    userManager.InitUserManager();

    const user = new User();
    user.GetAllUsers();
    user.InitUser();

    const module = new Module();
    module.GetAllModules();
    module.InitModule();
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

                $("#modalUserDetails").modal("hide");
            }).setHeader(`${CapitalizeWords(action)} user`);
        });

        $("#modalUserDetails").off().on("hidden.bs.modal", function () {
            $("#userModalTitle").text("Add User");
            $("#btnSubmitUser").html('<i aria-hidden="true" class="fa fa-plus"></i> Add User');
            $("#btnSubmitUser").data("action", "add");
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
    this.GetAllUsers = function () {
        $.post("/UserManager/GetAllUsers", function (response) {
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
                if (response.status === 2) {
                    reject(response.message);
                }
                else {
                    resolve(response.data);
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
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _user.PopulateUser(response.data);
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
                    Description: $("#Description").val(),
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
            $("#moduleModalTitle").text("Add Module");
            $("#btnSubmitModule").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Module');
            $("#btnSubmitModule").data("action", "add");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnModulePageToggle").off().on("click", function () {
            $("#btnClearModuleForm").trigger("click");
        });
    }

    this.GetAllModules = function () {
        $.post("/UserManager/GetAllModules", function (response) {
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
                if (response.status === 2) {
                    reject(response.message);
                }
                else {
                    resolve(response.data);
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
                                    <a class="remove-module active" data-module-id="${module.moduleId}" href="#"><i aria-hidden="true" class="fa fa-trash"></i> Remove</a>
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
            _module.GetModuleById($(this).data("module-id")).then(moduleData => {
                var module = moduleData[0];

                $("#modalModuleDetails").modal("show");
                $("#moduleModalTitle").text("Update Module");
                $("#btnSubmitModule").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Module');
                $("#btnSubmitModule").data("action", "update");
                $("#btnSubmitModule").data("id", $(this).data("module-id"));

                $("#ModuleName").val(module.moduleName);
                $("#ModulePage").val(module.modulePage);
                $("#Description").val(module.description);
                $("#Icon").val(module.icon);
                $("#txtSortNo").val(module.sortNo);
                $("#chkModuleStatus").bootstrapSwitch("state", module.isActive == 1 ? true : false);
            }).catch((err) => {
                Notif(`PopulateModule: ${err}`, "danger");
            });
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
}