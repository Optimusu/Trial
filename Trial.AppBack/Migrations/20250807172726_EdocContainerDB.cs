using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class EdocContainerDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EdocContainers",
                columns: table => new
                {
                    EdocContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    NameContainer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Studies_Protocol_CorporationId",
                table: "Studies",
                columns: new[] { "Protocol", "CorporationId" },
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EdocContainers");

            migrationBuilder.DropIndex(
                name: "IX_Studies_Protocol_CorporationId",
                table: "Studies");
        }
    }
}
