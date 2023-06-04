using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemAdminProducts.Migrations
{
    /// <inheritdoc />
    public partial class addDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreateAt", "Description", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(93), "Description Category 1", "Category 1", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(94) },
                    { 2, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(96), "Description Category 2", "Category 2", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(97) },
                    { 3, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(98), "Description Category 3", "Category 3", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(99) }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "CreateAt", "Email", "Name", "Phone", "UpdateAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(327), null, "Supplier 1", null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(328) },
                    { 2, null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(329), null, "Supplier 2", null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(330) },
                    { 3, null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(331), null, "Supplier 3", null, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(331) }
                });

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "Id", "CategoryId", "CreateAt", "Description", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(298), "Description SubCategory 1", "SubCategory 1", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(300) },
                    { 2, 2, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(302), "Description SubCategory 2", "SubCategory 2", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(302) },
                    { 3, 3, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(304), "Description SubCategory 3", "SubCategory 3", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(305) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CostPrice", "CreateAt", "Description", "Proffit", "SubCategoryId", "SupplierId", "UpcCode", "UpdateAt" },
                values: new object[,]
                {
                    { 1, null, 10.5, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(354), "Product 1", 1.5, 1, 1, "4314556", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(355) },
                    { 2, null, 10.5, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(358), "Product 2", 1.5, 2, 2, "31467885", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(359) },
                    { 3, null, 10.5, new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(361), "Product 3", 1.5, 3, 3, "12345623", new DateTime(2023, 6, 4, 18, 33, 6, 445, DateTimeKind.Local).AddTicks(361) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
