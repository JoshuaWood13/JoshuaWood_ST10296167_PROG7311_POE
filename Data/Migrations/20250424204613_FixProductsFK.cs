using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoshuaWood_ST10296167_PROG7311_POE.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductsFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_User",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_User",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.AddColumn<string>(
                name: "FarmerCode",
                table: "Products",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FarmerCode",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_User",
                table: "Products",
                column: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
