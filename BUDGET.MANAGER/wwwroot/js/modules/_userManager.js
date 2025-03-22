document.addEventListener("DOMContentLoaded", function () {
    const userManager = new UserManager();
    userManager.InitUserManager();

    const user = new User();
    user.GetAllUsers();
    user.InitUser();
});

function UserManager() {
    const _userManager = this;

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

function User() {
    var _user = this;

    this.InitUser = function () {
        $("#btnSubmitUser").off().on("click", function () {
            var form = $("#userForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");
            var id = $(this).data("id");
            var isActive = $("#chkUserStatus").prop("checked") === true ? 1 : 0;

            var userData = {
                Firstname: $("#Firstname").val(),
                Lastname: $("#Lastname").val(),
                Username: $("#Username").val(),
                Gender: $("#Gender").val(),
                IsActive: isActive
            };

            if (action == "update") {
                userData.UserId = id;

                _user.ModifyUser(userData);
            }
            else {
                userData.Password = $("#Password").val();
                _user.AddUser(userData);
            }

            $("#modalUserDetails").modal("hide");
        });

        $("#modalUserDetails").off().on("hidden.bs.modal", function () {
            $("#userModalTitle").text("Add User");
            $("#btnSubmitUser").html('<i aria-hidden="true" class="fa fa-plus"></i> Add User');
            $("#Password").prop("disabled", false);
        });

        $("#btnUserPageToggle").off().on("click", function () {
            $("#btnClearUserForm").trigger("click");
        });
    }

    this.GetAllUsers = function () {
        $.post("/UserManager/GetAllUsers", function (response) {
            if (response.status === 2) {
                console.log(response.message);
            }
            else {
                _user.PopulateUser(response.data);
            }
        });
    }

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

    this.PopulateUser = function (data) {
        $("#user-table tbody").empty();

        var row = "";

        if (data != null) {
            data.forEach(user => {
                row = `
                        <tr>
                            <td>
                                <span>${user.firstname} ${user.lastname}</span><br/>
                                <div class="nav-container">
                                    <a class="edit-user active" data-user-id="${user.userId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                                </div>
                            </td>
                            <td>${user.gender}</td>
                            <td>${user.username}</td>
                            <td><div class="user-status badge badge-pill ${user.isActive == 1 ? "badge-success" : "badge-danger"}">${user.isActive == 1 ? "Active" : "In Active"}</div></td>
                        </tr>
                    `;
            });
        }
        else {
            row = `<tr class="text-center">
                        <td colspan="4">No user data.</td>
                    </tr>`;
        }

        $("#user-table tbody").append(row);

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
                console.error(err);
            });
        });
    };

    this.AddUser = function (user) {
        $.post("/UserManager/AddUser", { user: user }, function (response) {
            if (response.status === 2) {
                console.log(response.message);
            }
            else {
                _user.PopulateUser(response.data);
            }
        });
    }

    this.ModifyUser = function (user) {
        $.post("/UserManager/ModifyUser", { user: user }, function (response) {
            if (response.status === 2) {
                console.log(response.message);
            }
            else {
                _user.PopulateUser(response.data);
            }
        });
    }
}