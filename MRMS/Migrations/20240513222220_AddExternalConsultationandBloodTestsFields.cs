using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalConsultationandBloodTestsFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchedPatientId",
                table: "ExternalConsultation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Imported",
                table: "ExternalBloodTest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MatchedPatientId",
                table: "ExternalBloodTest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExternalConsultationId",
                table: "BloodTest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "MatchedPatientId",
                table: "ExternalConsultation");

            migrationBuilder.DropColumn(
                name: "Imported",
                table: "ExternalBloodTest");

            migrationBuilder.DropColumn(
                name: "MatchedPatientId",
                table: "ExternalBloodTest");

            migrationBuilder.DropColumn(
                name: "ExternalConsultationId",
                table: "BloodTest");
        }
    }
}
