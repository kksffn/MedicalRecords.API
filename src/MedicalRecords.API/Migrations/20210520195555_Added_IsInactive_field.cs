using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalRecords.API.Migrations
{
    public partial class Added_IsInactive_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInactive",
                schema: "MedicalRecords",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInactive",
                schema: "MedicalRecords",
                table: "Patients");
        }
    }
}
