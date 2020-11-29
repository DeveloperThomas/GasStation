using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class removeTypeFromLoyaltyCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "LoyaltyCards");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "LoyaltyCards",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostalCode",
                table: "LoyaltyCards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "LoyaltyCards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
