using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebShop.Api.Migrations.Products
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "products");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "products",
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("15f0c532-f950-4616-a79c-eb88309f63f7"), "Product #5", 500m },
                    { new Guid("6187100d-9ed7-4e46-992e-766ea75814d4"), "Product #2", 200m },
                    { new Guid("94189f15-d4bb-4b1a-8a9f-118db8513919"), "Product #3", 300m },
                    { new Guid("d416be83-8c5e-425a-9a49-a21728dfd566"), "Product #1", 100m },
                    { new Guid("ef8b03c7-bbce-401a-861d-30eef1d8a453"), "Product #4", 400m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "products");
        }
    }
}
