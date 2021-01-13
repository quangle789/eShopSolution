using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 54, 45, 36, DateTimeKind.Local).AddTicks(7798),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 53, 31, 347, DateTimeKind.Local).AddTicks(6757));

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[] { 4, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "en-US", "Viet Tien men T-Shirt", 1, "viet-tien-men-t-shirt", "viet-tien-men-t-shirt", "viet-tien-men-t-shirt" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 54, 45, 52, DateTimeKind.Local).AddTicks(537));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 53, 31, 347, DateTimeKind.Local).AddTicks(6757),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 54, 45, 36, DateTimeKind.Local).AddTicks(7798));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 53, 31, 362, DateTimeKind.Local).AddTicks(3601));
        }
    }
}
