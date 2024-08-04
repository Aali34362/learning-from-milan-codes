using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF.ComplexTypes.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Books", x => x.Id));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
