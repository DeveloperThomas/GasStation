using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class changeInProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductsLists",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Discounts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "ProductsLists",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Products",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "Discounts",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
