using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class DeleteContainerNewCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EdocContainers");

            migrationBuilder.CreateTable(
                name: "EdocCategories",
                columns: table => new
                {
                    EdocCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameContainer = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdocCategories", x => x.EdocCategoryId);
                    table.ForeignKey(
                        name: "FK_EdocCategories_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EdocCategories_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "StudyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_CorporationId",
                table: "EdocCategories",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_EdocCategoryId",
                table: "EdocCategories",
                column: "EdocCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_Name_CorporationId",
                table: "EdocCategories",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_NameContainer",
                table: "EdocCategories",
                column: "NameContainer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EdocCategories_StudyId",
                table: "EdocCategories",
                column: "StudyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EdocCategories");

            migrationBuilder.CreateTable(
                name: "EdocContainers",
                columns: table => new
                {
                    EdocContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CorporationId = table.Column<int>(type: "int", nullable: false),
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameContainer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdocContainers", x => x.EdocContainerId);
                    table.ForeignKey(
                        name: "FK_EdocContainers_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EdocContainers_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "StudyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EdocContainers_CorporationId",
                table: "EdocContainers",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocContainers_EdocContainerId",
                table: "EdocContainers",
                column: "EdocContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocContainers_NameContainer",
                table: "EdocContainers",
                column: "NameContainer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EdocContainers_StudyId",
                table: "EdocContainers",
                column: "StudyId");
        }
    }
}