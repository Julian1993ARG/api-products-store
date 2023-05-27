using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemAdminProducts.Migrations
{
    /// <inheritdoc />
    public partial class descriptionIsIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_Decription",
                table: "Products",
                column: "Decription",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Decription",
                table: "Products");
        }
    }
}
