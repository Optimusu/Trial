using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class updateAllModals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cros_Corporations_CorporationId",
                table: "Cros");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollings_Corporations_CorporationId",
                table: "Enrollings");

            migrationBuilder.DropForeignKey(
                name: "FK_Indications_Corporations_CorporationId",
                table: "Indications");

            migrationBuilder.DropForeignKey(
                name: "FK_Irbs_Corporations_CorporationId",
                table: "Irbs");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_Corporations_CorporationId",
                table: "Sponsors");

            migrationBuilder.DropForeignKey(
                name: "FK_TherapeuticAreas_Corporations_CorporationId",
                table: "TherapeuticAreas");

            migrationBuilder.DropIndex(
                name: "IX_TherapeuticAreas_CorporationId",
                table: "TherapeuticAreas");

            migrationBuilder.DropIndex(
                name: "IX_TherapeuticAreas_Name_CorporationId",
                table: "TherapeuticAreas");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_CorporationId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_Name_CorporationId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Irbs_CorporationId",
                table: "Irbs");

            migrationBuilder.DropIndex(
                name: "IX_Irbs_Name_CorporationId",
                table: "Irbs");

            migrationBuilder.DropIndex(
                name: "IX_Indications_CorporationId",
                table: "Indications");

            migrationBuilder.DropIndex(
                name: "IX_Indications_Name_CorporationId",
                table: "Indications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollings_CorporationId",
                table: "Enrollings");

            migrationBuilder.DropIndex(
                name: "IX_Enrollings_Name_CorporationId",
                table: "Enrollings");

            migrationBuilder.DropIndex(
                name: "IX_Cros_CorporationId",
                table: "Cros");

            migrationBuilder.DropIndex(
                name: "IX_Cros_Name_CorporationId",
                table: "Cros");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "TherapeuticAreas");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Irbs");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Indications");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Enrollings");

            migrationBuilder.DropColumn(
                name: "CorporationId",
                table: "Cros");

            migrationBuilder.CreateIndex(
                name: "IX_TherapeuticAreas_Name",
                table: "TherapeuticAreas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_Name",
                table: "Sponsors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Irbs_Name",
                table: "Irbs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indications_Name",
                table: "Indications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_Name",
                table: "Enrollings",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cros_Name",
                table: "Cros",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TherapeuticAreas_Name",
                table: "TherapeuticAreas");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_Name",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Irbs_Name",
                table: "Irbs");

            migrationBuilder.DropIndex(
                name: "IX_Indications_Name",
                table: "Indications");

            migrationBuilder.DropIndex(
                name: "IX_Enrollings_Name",
                table: "Enrollings");

            migrationBuilder.DropIndex(
                name: "IX_Cros_Name",
                table: "Cros");

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "TherapeuticAreas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Sponsors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Irbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Indications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Enrollings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CorporationId",
                table: "Cros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TherapeuticAreas_CorporationId",
                table: "TherapeuticAreas",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapeuticAreas_Name_CorporationId",
                table: "TherapeuticAreas",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_CorporationId",
                table: "Sponsors",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_Name_CorporationId",
                table: "Sponsors",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Irbs_CorporationId",
                table: "Irbs",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Irbs_Name_CorporationId",
                table: "Irbs",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indications_CorporationId",
                table: "Indications",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Indications_Name_CorporationId",
                table: "Indications",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_CorporationId",
                table: "Enrollings",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_Name_CorporationId",
                table: "Enrollings",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cros_CorporationId",
                table: "Cros",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cros_Name_CorporationId",
                table: "Cros",
                columns: new[] { "Name", "CorporationId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cros_Corporations_CorporationId",
                table: "Cros",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollings_Corporations_CorporationId",
                table: "Enrollings",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Indications_Corporations_CorporationId",
                table: "Indications",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Irbs_Corporations_CorporationId",
                table: "Irbs",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_Corporations_CorporationId",
                table: "Sponsors",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TherapeuticAreas_Corporations_CorporationId",
                table: "TherapeuticAreas",
                column: "CorporationId",
                principalTable: "Corporations",
                principalColumn: "CorporationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
