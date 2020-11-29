using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class LoyaltyCardIdAsNullableFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_LoyaltyCards_LoyaltyCardId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "LoyaltyCardId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_LoyaltyCards_LoyaltyCardId",
                table: "Transactions",
                column: "LoyaltyCardId",
                principalTable: "LoyaltyCards",
                principalColumn: "LoyaltyCardId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_LoyaltyCards_LoyaltyCardId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "LoyaltyCardId",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_LoyaltyCards_LoyaltyCardId",
                table: "Transactions",
                column: "LoyaltyCardId",
                principalTable: "LoyaltyCards",
                principalColumn: "LoyaltyCardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
