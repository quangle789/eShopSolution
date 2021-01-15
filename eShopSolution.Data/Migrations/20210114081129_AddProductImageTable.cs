using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 1, 13, 16, 41, 29, 17, DateTimeKind.Local).AddTicks(2842));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefualt = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                column: "ConcurrencyStamp",
                value: "97698fa2-c911-42b1-ade2-d208da1feac7");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c5be69de-dd3f-491d-88c9-d69e4edf8307", "AQAAAAEAACcQAAAAEHPiiMzMj6GCSJ/4kppw5YGywGuNYfkwFUvwbf7CGaCmD0FdRucxcgJ+zzUr7OM3rA==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 14, 15, 11, 28, 802, DateTimeKind.Local).AddTicks(2259));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 1, 13, 16, 41, 29, 17, DateTimeKind.Local).AddTicks(2842),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                column: "ConcurrencyStamp",
                value: "b8474094-50bd-4b2a-acca-fb6542aeb5a4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9806e83-80f2-447c-a8fb-6da039029af7", "AQAAAAEAACcQAAAAEEnYCIXgWIyMxNlqtfZrWluDm9XbEnVkaaGW6JzmWAn3L38MujD87hJ/w5nKzcV70Q==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 1, 13, 16, 41, 29, 37, DateTimeKind.Local).AddTicks(2206));
        }
    }
}
