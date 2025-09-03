// @ts-nocheck
document.addEventListener("DOMContentLoaded", function () {
    const category = new Category();

    category.InitCategory();

    const budget = new BudgetCategory();

    budget.GetAllBudgetCategory(2);
    budget.InitBudgetCategory();
});

function Category() {
    const _category = this;

    this.InitCategory = function () {
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
                case "btnBudgetCategory":
                    const budget = new BudgetCategory();
                    budget.GetAllBudgetCategory(2);
                    budget.InitBudgetCategory();
                //case "btnRoles":
                //    const role = new Role();
                //    role.GetAllRoles(2);
                //    role.InitRole();
            }
        });
    }
}

function BudgetCategory() {
    const _budgetCategory = this;

    this.InitBudgetCategory = function () {
        $("#btnSubmitBudgetCategory").data("action", "add");
        $("#btnSubmitBudgetCategory").off().on("click", function () {
            var form = $("#budgetCategoryForm");

            if (!form.valid()) {
                return;
            }

            var action = $(this).data("action");

            alertify.confirm(`Are you sure you want to ${action} this budget category?`, function (e) {
                var isActive = $("#chkBudgetCategoryStatus").prop("checked") === true ? 1 : 0; 
                var budgetCategory = {
                    BudgetCategoryName: $("#BudgetCategoryName").val().trim(),
                    BudgetCategoryDescription: $("#BudgetCategoryDescription").val().trim(),
                    IsActive: isActive
                };

                if (action == "add") {
                    _budgetCategory.AddBudgetCategory(budgetCategory);
                }
                else {
                    budgetCategory.BudgetCategoryId = $("#btnSubmitBudgetCategory").data("id");
                    _budgetCategory.ModifyBudgetCategory(budgetCategory);
                }
            });
        });

        $("#modalBudgetCategoryDetails").off().on("hidden.bs.modal", function () {
            $("#budgetCategoryTitle").text("New Budget Category");
            $("#btnSubmitBudgetCategory").html('<i aria-hidden="true" class="fa fa-plus"></i> Add Budget Category');
            $("#btnSubmitBudgetCategory").data("action", "add");
            $("#btnSubmitBudgetCategory").removeAttr("data-id");
            $(this).find('.field-validation-error, .field-validation-valid').children().remove();
        });

        $("#btnBudgetPageToggle").off().on("click", function () {
            $("#btnClearBudgetCategoryForm").trigger("click");
        });
    }

    this.GetAllBudgetCategory = function (status) {
        $.post("/Category/GetAllBudgetCategories", { status: status }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                _budgetCategory.PopulateBudgetCategory(response.data);
            }
        });
    }

    this.GetBudgetCategoryById = function (budgetCategoryId) {
        return new Promise((resolve, reject) => {
            $.post("/Category/GetBudgetCategoryById", { budgetCategoryId: budgetCategoryId }, function (response) {
                if (response.status === 1) {
                    resolve(response.data);
                }
                else {
                    reject(response.message);
                }
            });
        });
    }

    this.PopulateBudgetCategory = function (data) {
        const table = $("#tblBudgetCategory tbody");

        table.empty();

        if (data != null) {
            data.forEach(budget => {
                const row = `
                    <tr>
                        <td>
                            <span>${budget.budgetCategoryName}</span><br/>
                            <div class="nav-container">
                                <a class="edit-budget-category active" data-budget-category-id="${budget.budgetCategoryId}" href="#"><i aria-hidden="true" class="fa fa-pencil"></i> Edit</a>
                            </div>
                        </td>
                        <td>${budget.budgetCategoryDescription}</td>
                        <td><div class="budget-category-status badge badge-pill ${budget.isActive == 1 ? "badge-success" : "badge-danger"}">${budget.isActive == 1 ? "Active" : "Inactive"}</div></td>
                    </tr>
                `;

                table.append(row);
            });

            $(".edit-budget-category").on("click", function () {
                _budgetCategory.GetBudgetCategoryById($(this).data("budget-category-id")).then((budgetData) => {
                    var data = budgetData[0];

                    $("#modalBudgetCategoryDetails").modal("show");
                    $("#budgetCategoryTitle").text("Update Budget Category");
                    $("#btnSubmitBudgetCategory").html('<i aria-hidden="true" class="fa fa-pencil"></i> Update Budget Category');
                    $("#btnSubmitBudgetCategory").data("action", "update");
                    $("#btnSubmitBudgetCategory").data("id", $(this).data("budget-category-id"));

                    $("#BudgetCategoryName").val(data.budgetCategoryName);
                    $("#BudgetCategoryDescription").val(data.budgetCategoryDescription);
                    $("#chkBudgetCategoryStatus").bootstrapSwitch("state", data.isActive == 1 ? true : false);
                }).catch((err) => {
                    Notif(err, "danger");
                });
            });
        }
        else {
            table.append(`<tr class="text-center">
                        <td colspan="4">No budget category data found.</td>
                    </tr>`);
        }
    }

    this.AddBudgetCategory = function (budgetCategory) {
        $.post("/Category/AddBudgetCategory", { budgetCategory: budgetCategory }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _budgetCategory.PopulateBudgetCategory(response.data);
                $("#modalBudgetCategoryDetails").modal("hide");
            }
        });
    }

    this.ModifyBudgetCategory = function (budgetCategory) {
        $.post("/Category/ModifyBudgetCategory", { budgetCategory: budgetCategory }, function (response) {
            if (response.status === 2) {
                Notif(response.message, "danger");
            }
            else {
                Notif(response.message, "success");
                _budgetCategory.PopulateBudgetCategory(response.data);
                $("#modalBudgetCategoryDetails").modal("hide");
            }
        });
    }
}