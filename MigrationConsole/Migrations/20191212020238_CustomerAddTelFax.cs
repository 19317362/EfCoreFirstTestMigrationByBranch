using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationConsole.Migrations
{
    public partial class CustomerAddTelFax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Customers");
        }
    }
}
