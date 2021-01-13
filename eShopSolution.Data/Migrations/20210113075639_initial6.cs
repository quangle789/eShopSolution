using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 56, 38, 480, DateTimeKind.Local).AddTicks(9644),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 54, 45, 36, DateTimeKind.Local).AddTicks(7798));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 54, 45, 36, DateTimeKind.Local).AddTicks(7798),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 56, 38, 480, DateTimeKind.Local).AddTicks(9644));

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "en-US", "Viet Tien men T-Shirt", 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" },
                    { 3, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "en-US", "Viet Tien men T-Shirt", 2, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" },
                    { 4, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "en-US", "Viet Tien men T-Shirt", 1, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 54, 45, 52, DateTimeKind.Local).AddTicks(537));
        }
    }
}
