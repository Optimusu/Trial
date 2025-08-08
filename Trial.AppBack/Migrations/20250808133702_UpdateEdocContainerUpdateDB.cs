using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEdocContainerUpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EdocCategories_NameContainer",
                table: "EdocCategories");

            migrationBuilder.AlterColumn<string>(
                name: "NameContainer",
                table: "EdocCategories",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63);

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_NameContainer",
                table: "EdocCategories",
                column: "NameContainer",
                unique: true,
                filter: "[NameContainer] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EdocCategories_NameContainer",
                table: "EdocCategories");

            migrationBuilder.AlterColumn<string>(
                name: "NameContainer",
                table: "EdocCategories",
                type: "nvarchar(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(63)",
                oldMaxLength: 63,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_NameContainer",
                table: "EdocCategories",
                column: "NameContainer",
                unique: true);
        }
    }
}
