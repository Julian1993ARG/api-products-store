using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemAdminProducts.Migrations
{
    /// <inheritdoc />
    public partial class addCostPriceAndProffictPropertiProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "Proffit");

            migrationBuilder.AddColumn<double>(
                name: "CostPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Proffit",
                table: "Products",
                newName: "Price");
        }
    }
}
