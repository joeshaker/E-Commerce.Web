using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class LastOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_TypeID",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "Products",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Products",
                newName: "BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeID",
                table: "Products",
                newName: "IX_Products_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandID",
                table: "Products",
                newName: "IX_Products_BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "ProductBrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products",
                column: "TypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_TypeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Products",
                newName: "TypeID");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "Products",
                newName: "BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_TypeId",
                table: "Products",
                newName: "IX_Products_TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                newName: "IX_Products_BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_BrandID",
                table: "Products",
                column: "BrandID",
                principalTable: "ProductBrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_TypeID",
                table: "Products",
                column: "TypeID",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }
    }
}
