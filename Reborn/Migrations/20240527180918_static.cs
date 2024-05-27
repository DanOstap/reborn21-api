using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reborn.Migrations
{
    /// <inheritdoc />
    public partial class @static : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Productid",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_Productid",
                table: "orders",
                column: "Productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Userid",
                table: "orders",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_products_Productid",
                table: "orders",
                column: "Productid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_Userid",
                table: "orders",
                column: "Userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_products_Productid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_Userid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_Productid",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_Userid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "image",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Productid",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "orders");
        }
    }
}
