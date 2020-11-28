using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class removeDistributorFromProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors");

            migrationBuilder.DropIndex(
                name: "IX_Distributors_ProductId",
                table: "Distributors");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Distributors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Distributors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_ProductId",
                table: "Distributors",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
