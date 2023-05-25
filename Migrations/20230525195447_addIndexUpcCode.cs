using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemAdminProducts.Migrations
{
    /// <inheritdoc />
    public partial class addIndexUpcCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_UpcCode",
                table: "Products",
                column: "UpcCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_UpcCode",
                table: "Products");
        }
    }
}
