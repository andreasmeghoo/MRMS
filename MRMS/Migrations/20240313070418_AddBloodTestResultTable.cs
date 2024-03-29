using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class AddBloodTestResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodTestResult",
                columns: table => new
                {
                    BloodTestResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodTestId = table.Column<int>(type: "int", nullable: false),
                    redBloodCellCount = table.Column<int>(type: "int", nullable: false),
                    whiteBloodCellCount = table.Column<int>(type: "int", nullable: false),
                    plateletCount = table.Column<int>(type: "int", nullable: false),
                    glucoseLevel = table.Column<int>(type: "int", nullable: false),
                    cholestorolLevel = table.Column<int>(type: "int", nullable: false),
                    liverFunction = table.Column<int>(type: "int", nullable: false),
                    kidneyFunction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTestResult", x => x.BloodTestResultId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodTestResult");
        }
    }
}
