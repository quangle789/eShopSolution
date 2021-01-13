using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 13, 34, 13, 154, DateTimeKind.Local).AddTicks(9157),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 13, 32, 53, 961, DateTimeKind.Local).AddTicks(5802));

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[] { 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "en-US", "Viet Tien men T-Shirt", 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 13, 34, 13, 170, DateTimeKind.Local).AddTicks(5682));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 13, 32, 53, 961, DateTimeKind.Local).AddTicks(5802),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 13, 13, 34, 13, 154, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 13, 32, 53, 975, DateTimeKind.Local).AddTicks(2829));
        }
    }
}
