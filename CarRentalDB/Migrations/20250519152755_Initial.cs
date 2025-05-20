using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRentalDB.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rule = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    RentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.RentId);
                    table.ForeignKey(
                        name: "FK_Rents_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Color", "Model", "PlateNumber" },
                values: new object[,]
                {
                    { 1, "Red", "Toyota Corolla", "ABC123" },
                    { 2, "Blue", "Honda Civic", "XYZ789" },
                    { 3, "Black", "Ford Mustang", "LMN456" },
                    { 4, "White", "Chevrolet Malibu", "JKL321" },
                    { 5, "Silver", "Nissan Altima", "DEF654" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Address2", "City", "County", "DateOfBirth", "Email", "FirstName", "LastName", "LicenseNumber", "Password", "PhoneNumber", "Rule" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Apt 1", "New York", "NY", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John", "Doe", "NY123456", "hashedpwd1", "1234567890", 1 },
                    { 2, "456 Broadway", "Nablus", "Chicago", "IL", new DateTime(1985, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane", "Smith", "IL789456", "hashedpwd2", "0987654321", 1 },
                    { 3, "789 Sunset Blvd", "Ramallah", "Los Angeles", "CA", new DateTime(1992, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ali.khan@example.com", "Ali", "Khan", "CA456789", "hashedpwd3", "5551112233", 2 },
                    { 4, "321 Palm Ave", "Suite 10", "Miami", "FL", new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.lee@example.com", "Sara", "Lee", "FL112233", "hashedpwd4", "4445556666", 1 },
                    { 5, "101 Desert Rd", "IDk", "Phoenix", "AZ", new DateTime(1988, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "mohammed.zayed@example.com", "Mohammed", "Zayed", "AZ778899", "hashedpwd5", "3334445555", 1 }
                });

            migrationBuilder.InsertData(
                table: "Rents",
                columns: new[] { "RentId", "CarId", "RentDate", "ReturnDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 4, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 5, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_CarId",
                table: "Rents",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_UserId",
                table: "Rents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
