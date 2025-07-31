using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocumentAllNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_Corporations_CorporationId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_CorporationId",
                table: "DocumentTypes");

            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_DocumentName_CorporationId",
                table: "DocumentTypes");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "DocumentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DocumentName",
                table: "DocumentTypes",
                column: "DocumentName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentTypes_DocumentName",
                table: "DocumentTypes");

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "DocumentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_CorporationId",
                table: "DocumentTypes",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DocumentName_CorporationId",
                table: "DocumentTypes",
                columns: new[] { "DocumentName", "CorporationId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_Corporations_CorporationId",
                table: "DocumentTypes",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
