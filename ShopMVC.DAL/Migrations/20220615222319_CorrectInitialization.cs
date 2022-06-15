using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMVC.DAL.Migrations
{
    public partial class CorrectInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "dbo",
                table: "PurchaseComposition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "dbo",
                table: "PurchaseComposition",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
