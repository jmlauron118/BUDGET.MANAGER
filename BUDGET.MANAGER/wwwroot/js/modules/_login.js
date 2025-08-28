// @ts-nocheck
document.addEventListener("DOMContentLoaded", function () {
    const login = new Login();
    login.InitLogin();
});

function Login() {
    const _login = this;

    this.InitLogin = function () {
        $("#LoginUsername").val("administrator");
        $("#LoginPassword").val("@dm1n011896");

        $("#btnLogin").off().on("click", function () {
            var form = $("#loginForm");

            if (!form.valid()) {
                return;
            }

            var username = $("#LoginUsername").val().trim();
            var password = $("#LoginPassword").val().trim();
            
            _login.GetLogginUser(username, password);
        });
    }

    this.GetLogginUser = function (username, password) {
        $.post("/Login/GetLogginUser", { username: username, password: password }, function (response) {
            if (response.status == 1) {
                window.location.href = `/${response.data.moduleName}/Index`;
            }
            else {
                $("#LoginPassword").val("");
                Notif(response.message, "danger");
            }
        });
    }
}