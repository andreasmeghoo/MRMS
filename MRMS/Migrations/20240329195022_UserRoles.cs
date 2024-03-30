using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MRMS.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "061df403-1b70-467a-a739-0b14ed4afdb3", null, "admin", "admin" },
                    { "6a5b27c7-e859-46c6-b490-4b734cf380e0", null, "receptionist", "receptionist" },
                    { "777d4654-ea2c-44c0-95e6-7aa8b091aa08", null, "patient", "patient" },
                    { "7f598720-dc8b-49a0-9136-15484948a37a", null, "doctor", "doctor" },
                    { "f212c378-9f8b-4c14-afc4-85d2990635d9", null, "nurse", "nurse" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "061df403-1b70-467a-a739-0b14ed4afdb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a5b27c7-e859-46c6-b490-4b734cf380e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "777d4654-ea2c-44c0-95e6-7aa8b091aa08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f598720-dc8b-49a0-9136-15484948a37a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f212c378-9f8b-4c14-afc4-85d2990635d9");
        }
    }
}
