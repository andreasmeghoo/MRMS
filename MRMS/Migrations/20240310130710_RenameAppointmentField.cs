using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class RenameAppointmentField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointment",
                newName: "PreferredDoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferredDoctorId",
                table: "Appointment",
                newName: "DoctorId");
        }
    }
}
