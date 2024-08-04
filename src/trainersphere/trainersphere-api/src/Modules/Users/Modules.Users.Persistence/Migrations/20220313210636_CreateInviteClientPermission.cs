﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Users.Persistence.Migrations
{
    public partial class CreateInviteClientPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "users",
                table: "permissions",
                columns: new[] { "id", "description", "name" },
                values: new object[] { 200, "Can invite new client.", "InviteClient" });

            migrationBuilder.InsertData(
                schema: "users",
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[] { 200, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "role_permissions",
                keyColumns: new[] { "permission_id", "role_id" },
                keyValues: new object[] { 200, 2 });

            migrationBuilder.DeleteData(
                schema: "users",
                table: "permissions",
                keyColumn: "id",
                keyValue: 200);
        }
    }
}
