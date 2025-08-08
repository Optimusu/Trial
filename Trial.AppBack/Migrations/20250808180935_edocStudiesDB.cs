using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class edocStudiesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EdocStudies",
                columns: table => new
                {
                    EdocStudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EdocCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FileNameOriginal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signing = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdocStudies", x => x.EdocStudyId);
                    table.ForeignKey(
                        name: "FK_EdocStudies_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EdocStudies_EdocCategories_EdocCategoryId",
                        column: x => x.EdocCategoryId,
                        principalTable: "EdocCategories",
                        principalColumn: "EdocCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EdocStudies_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "StudyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EdocStudies_CorporationId",
                table: "EdocStudies",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocStudies_EdocCategoryId",
                table: "EdocStudies",
                column: "EdocCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocStudies_EdocStudyId",
                table: "EdocStudies",
                column: "EdocStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_EdocStudies_FileNameOriginal_CorporationId",
                table: "EdocStudies",
                columns: new[] { "FileNameOriginal", "CorporationId" },
                unique: true,
                filter: "[FileNameOriginal] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EdocStudies_StudyId",
                table: "EdocStudies",
                column: "StudyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EdocStudies");
        }
    }
}
