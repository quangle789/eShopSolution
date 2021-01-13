using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 58, 2, 664, DateTimeKind.Local).AddTicks(6811),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 56, 38, 480, DateTimeKind.Local).AddTicks(9644));

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[] { 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "vi-VN", "Việt Tiến men T-Shirt", 1, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 58, 2, 680, DateTimeKind.Local).AddTicks(5929));
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
                defaultValue: new DateTime(2021, 1, 13, 14, 56, 38, 480, DateTimeKind.Local).AddTicks(9644),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 58, 2, 664, DateTimeKind.Local).AddTicks(6811));

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[] { 5, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "vi-VN", "Việt Tiến men T-Shirt", 1, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 56, 38, 496, DateTimeKind.Local).AddTicks(4931));
        }
    }
}
