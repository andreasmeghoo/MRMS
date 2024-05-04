using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class RenameBloodTestPerformedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PerformedById",
                table: "ExternalBloodTest",
                newName: "PerformedBy");
         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.RenameColumn(
                name: "PerformedBy",
                table: "ExternalBloodTest",
                newName: "PerformedById");
        }
    }
}
