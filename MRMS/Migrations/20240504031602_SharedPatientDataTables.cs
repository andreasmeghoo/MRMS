using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class SharedPatientDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {        
            migrationBuilder.CreateTable(
                name: "ExternalBloodTest",
                columns: table => new
                {
                    ExternalBloodTestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAddressLineOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAddressLineTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformedById = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redBloodCellCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    whiteBloodCellCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    plateletCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    glucoseLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    cholestorolLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    liverFunction = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    kidneyFunction = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalBloodTest", x => x.ExternalBloodTestId);
                });

            migrationBuilder.CreateTable(
                name: "ExternalConsultation",
                columns: table => new
                {
                    ExternalConsultationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAddressLineOne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAddressLineTwo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarePlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalConsultation", x => x.ExternalConsultationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalBloodTest");

            migrationBuilder.DropTable(
                name: "ExternalConsultation");
        }
    }
}
