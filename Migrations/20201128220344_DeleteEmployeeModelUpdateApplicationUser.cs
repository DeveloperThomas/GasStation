using Microsoft.EntityFrameworkCore.Migrations;

namespace GasStation.Migrations
{
    public partial class DeleteEmployeeModelUpdateApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Employees_EmployeeId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_EmployeeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_ApplicationUserId",
                table: "Transactions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_ApplicationUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ApplicationUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EmployeeId",
                table: "Transactions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Employees_EmployeeId",
                table: "Transactions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
