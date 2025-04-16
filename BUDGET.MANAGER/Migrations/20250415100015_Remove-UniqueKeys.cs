using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUDGET.MANAGER.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserRoles_UserId_RoleId",
                table: "UserRoles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ModuleActions_ModuleId_ActionId",
                table: "ModuleActions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ModuleAccess_ModuleActionId_UserRoleId",
                table: "ModuleAccess");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Actions_ActionName",
                table: "Actions");

            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Actions",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActionName",
                table: "Actions",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ModuleActions_ModuleId_ActionId",
                table: "ModuleActions",
                columns: new[] { "ModuleId", "ActionId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ModuleAccess_ModuleActionId_UserRoleId",
                table: "ModuleAccess",
                columns: new[] { "ModuleActionId", "UserRoleId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Actions_ActionName",
                table: "Actions",
                column: "ActionName");
        }
    }
}
