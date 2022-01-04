using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMC.Data.Migrations
{
    public partial class RemoveDrugCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Drugs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Drugs",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
