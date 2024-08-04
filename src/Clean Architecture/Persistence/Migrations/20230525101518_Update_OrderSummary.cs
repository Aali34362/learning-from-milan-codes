using System.Collections.Generic;
using Domain.Orders;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_OrderSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customer_name",
                table: "order_summaries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<OrderSummary.LineItem>>(
                name: "line_items",
                table: "order_summaries",
                type: "jsonb",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customer_name",
                table: "order_summaries");

            migrationBuilder.DropColumn(
                name: "line_items",
                table: "order_summaries");
        }
    }
}
