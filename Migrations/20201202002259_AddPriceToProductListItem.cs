using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class AddPriceToProductListItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "ProductsLists",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductsLists");
        }
    }
}
