using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Lastfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Product_ProductId",
                table: "Issue");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Issue",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_ProductId",
                table: "Issue",
                newName: "IX_Issue_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Order_OrderId",
                table: "Issue",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Order_OrderId",
                table: "Issue");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Issue",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Issue_OrderId",
                table: "Issue",
                newName: "IX_Issue_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Product_ProductId",
                table: "Issue",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
