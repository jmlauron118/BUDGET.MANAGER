using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BUDGET.MANAGER.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueKeyForAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "AK_Actions_ActionName",
                table: "Actions",
                column: "ActionName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
