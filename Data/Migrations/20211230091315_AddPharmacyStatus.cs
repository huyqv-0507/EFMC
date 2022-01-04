using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMC.Data.Migrations
{
    public partial class AddPharmacyStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Pharmacies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pharmacies");
        }
    }
}
