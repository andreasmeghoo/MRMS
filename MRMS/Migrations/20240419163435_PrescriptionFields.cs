using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class PrescriptionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Prescription",
                newName: "DoseStrengthUnits");

            migrationBuilder.RenameColumn(
                name: "DoseAmount",
                table: "Prescription",
                newName: "DoseStrength");

            migrationBuilder.AddColumn<int>(
                name: "DoseQuantity",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoseQuantityUnits",
                table: "Prescription",
                type: "int",
                nullable: false,
                defaultValue: 0);
       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.DropColumn(
                name: "DoseQuantity",
                table: "Prescription");

            migrationBuilder.DropColumn(
                name: "DoseQuantityUnits",
                table: "Prescription");

            migrationBuilder.RenameColumn(
                name: "DoseStrengthUnits",
                table: "Prescription",
                newName: "Strength");

            migrationBuilder.RenameColumn(
                name: "DoseStrength",
                table: "Prescription",
                newName: "DoseAmount");          
        }
    }
}
