using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalRecords.API.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MedicalRecords");

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nchar(9)", fixedLength: true, maxLength: 9, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskFactors",
                schema: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Factor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientRiskFactors",
                schema: "MedicalRecords",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RiskFactorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRiskFactors", x => new { x.PatientId, x.RiskFactorId });
                    table.ForeignKey(
                        name: "FK_PatientRiskFactors_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "MedicalRecords",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRiskFactors_RiskFactors_RiskFactorId",
                        column: x => x.RiskFactorId,
                        principalSchema: "MedicalRecords",
                        principalTable: "RiskFactors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientRiskFactors_RiskFactorId",
                schema: "MedicalRecords",
                table: "PatientRiskFactors",
                column: "RiskFactorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientRiskFactors",
                schema: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "RiskFactors",
                schema: "MedicalRecords");
        }
    }
}
