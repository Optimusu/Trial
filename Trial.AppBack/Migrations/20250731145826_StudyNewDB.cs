using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trial.AppBack.Migrations
{
    /// <inheritdoc />
    public partial class StudyNewDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Studies",
                columns: table => new
                {
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SiteNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    StudyNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TherapeuticAreaId = table.Column<int>(type: "int", nullable: false),
                    EnrollingId = table.Column<int>(type: "int", nullable: false),
                    IndicationId = table.Column<int>(type: "int", nullable: false),
                    TrialPhase = table.Column<int>(type: "int", nullable: false),
                    SponsorId = table.Column<int>(type: "int", nullable: false),
                    Protocol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "Latin1_General_CI_AS"),
                    CompleteProtocol = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ClinicalDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    IrbId = table.Column<int>(type: "int", nullable: false),
                    CroId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentGoal = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CorporationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studies", x => x.StudyId);
                    table.ForeignKey(
                        name: "FK_Studies_Corporations_CorporationId",
                        column: x => x.CorporationId,
                        principalTable: "Corporations",
                        principalColumn: "CorporationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Studies_Cros_CroId",
                        column: x => x.CroId,
                        principalTable: "Cros",
                        principalColumn: "CroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_Enrollings_EnrollingId",
                        column: x => x.EnrollingId,
                        principalTable: "Enrollings",
                        principalColumn: "EnrollingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_Indications_IndicationId",
                        column: x => x.IndicationId,
                        principalTable: "Indications",
                        principalColumn: "IndicationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_Irbs_IrbId",
                        column: x => x.IrbId,
                        principalTable: "Irbs",
                        principalColumn: "IrbId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_Sponsors_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsors",
                        principalColumn: "SponsorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_TherapeuticAreas_TherapeuticAreaId",
                        column: x => x.TherapeuticAreaId,
                        principalTable: "TherapeuticAreas",
                        principalColumn: "TherapeuticAreaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Studies_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_CorporationId",
                table: "Studies",
                column: "CorporationId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_CroId",
                table: "Studies",
                column: "CroId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_EnrollingId",
                table: "Studies",
                column: "EnrollingId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_IndicationId",
                table: "Studies",
                column: "IndicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_IrbId",
                table: "Studies",
                column: "IrbId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_SponsorId",
                table: "Studies",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyId",
                table: "Studies",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyNumber_CorporationId",
                table: "Studies",
                columns: new[] { "StudyNumber", "CorporationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studies_TherapeuticAreaId",
                table: "Studies",
                column: "TherapeuticAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_UsuarioId",
                table: "Studies",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Studies");
        }
    }
}
