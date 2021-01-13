using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 14, 51, 55, 455, DateTimeKind.Local).AddTicks(3670),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 13, 34, 13, 154, DateTimeKind.Local).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 14, 51, 55, 470, DateTimeKind.Local).AddTicks(4131));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 13, 34, 13, 154, DateTimeKind.Local).AddTicks(9157),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 1, 13, 14, 51, 55, 455, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 13, 34, 13, 170, DateTimeKind.Local).AddTicks(5682));
        }
    }
}
