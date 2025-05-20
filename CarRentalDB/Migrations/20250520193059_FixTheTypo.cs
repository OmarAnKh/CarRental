using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalDB.Migrations
{
    /// <inheritdoc />
    public partial class FixTheTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rule",
                table: "Users",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Rule");
        }
    }
}
