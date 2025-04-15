using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "ProductBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "ProductBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
