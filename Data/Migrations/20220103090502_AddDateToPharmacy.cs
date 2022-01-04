using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFMC.Data.Migrations
{
    public partial class AddDateToPharmacy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Pharmacies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Pharmacies");
        }
    }
}
