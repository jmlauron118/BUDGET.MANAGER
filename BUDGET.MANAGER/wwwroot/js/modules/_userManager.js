document.addEventListener("DOMContentLoaded", function () {
    const userManager = new UserManager();
    userManager.InitUserManager();
});

function UserManager() {
    const _userManager = this;

    this.InitUserManager = function () {
        const btnElements = document.querySelectorAll(".list-group-item");

        btnElements.forEach((element) => {
            element.addEventListener("click", function (e) {
                var id = (e.target.id).replace("btn","");
                var targetSelector = id.toLowerCase() + "-table";
                
                _userManager.LoadContent(id, "#" + targetSelector);
            });
        });

        let defaulTab = document.querySelector("#btnUsers");

        defaulTab.click();
    }

    this.LoadContent = function (type, targetSelector) {
        fetch("/UserManager/Load" + type)
            .then(response => response.text())
            .then(html => {
                document.querySelector(targetSelector).innerHTML = html; 

                //document.getElementById("userForm").addEventListener("submit", function (e) {
                //    e.preventDefault();

                //    _userManager.AddUser();
                //});
            })
            .catch((error) => {
                console.error(`Error loading content: ${error}`);
            });
    }

    this.AddUser = function () {
        const userForm = document.getElementById("userForm");
        const formData = new FormData(userForm);

        fetch("/UserManager/AddUser", {
            method: "POST",
            body: formData
        })
            .then(response => response.text())
            .then(html => {
                document.getElementById("users-table").innerHTML = html;
            })
            .catch((error) => {
                console.error(`Error adding user: ${error}`);
            });
    }
}