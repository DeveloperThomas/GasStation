using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class newFieldsFuelingProductDistributor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors");

            migrationBuilder.AddColumn<bool>(
                name: "Is_Fuel",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Fuelings",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "PriceInPoints",
                table: "Fuelings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TankId",
                table: "Fuelings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Distributors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Fuelings_TankId",
                table: "Fuelings",
                column: "TankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fuelings_Tanks_TankId",
                table: "Fuelings",
                column: "TankId",
                principalTable: "Tanks",
                principalColumn: "TankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors");

            migrationBuilder.DropForeignKey(
                name: "FK_Fuelings_Tanks_TankId",
                table: "Fuelings");

            migrationBuilder.DropIndex(
                name: "IX_Fuelings_TankId",
                table: "Fuelings");

            migrationBuilder.DropColumn(
                name: "Is_Fuel",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Fuelings");

            migrationBuilder.DropColumn(
                name: "PriceInPoints",
                table: "Fuelings");

            migrationBuilder.DropColumn(
                name: "TankId",
                table: "Fuelings");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Distributors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Distributors_Products_ProductId",
                table: "Distributors",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
