using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemAdminProducts.Migrations
{
    /// <inheritdoc />
    public partial class mistakeDescriptionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Decription",
                table: "Products",
                newName: "IX_Products_Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Decription");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Description",
                table: "Products",
                newName: "IX_Products_Decription");
        }
    }
}
