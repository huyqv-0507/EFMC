using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMC.Data.Migrations
{
    public partial class addnameuserpharmacies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "UserPharmacies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PharmacistName",
                table: "UserPharmacies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "UserPharmacies");

            migrationBuilder.DropColumn(
                name: "PharmacistName",
                table: "UserPharmacies");
        }
    }
}
