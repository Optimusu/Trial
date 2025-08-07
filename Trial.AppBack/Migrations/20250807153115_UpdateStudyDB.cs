using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullStudy",
                table: "Studies",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullStudy",
                table: "Studies");
        }
    }
}
