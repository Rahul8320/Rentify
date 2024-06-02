using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rentity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesandProprtyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Owner = table.Column<Guid>(type: "TEXT", nullable: false),
                    Place = table.Column<string>(type: "TEXT", nullable: false),
                    NoOfBedroom = table.Column<int>(type: "INTEGER", nullable: false),
                    NoOfBathroom = table.Column<int>(type: "INTEGER", nullable: false),
                    SizeinSqft = table.Column<int>(type: "INTEGER", nullable: false),
                    NearbySchool = table.Column<bool>(type: "INTEGER", nullable: false),
                    NearbyCollege = table.Column<bool>(type: "INTEGER", nullable: false),
                    NearbyHospital = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Likes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a9769e8e-63ca-408e-bdb0-69cec9c7c019", null, "Buyer", "BUYER" },
                    { "bf311099-0d68-475a-b479-82206f96f66f", null, "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9769e8e-63ca-408e-bdb0-69cec9c7c019");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf311099-0d68-475a-b479-82206f96f66f");
        }
    }
}
