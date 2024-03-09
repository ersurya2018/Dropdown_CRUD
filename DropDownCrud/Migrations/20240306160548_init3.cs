using Microsoft.EntityFrameworkCore.Migrations;

namespace DropDownCrud.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Cities",
                newName: "DistricName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistricName",
                table: "Cities",
                newName: "CityName");
        }
    }
}
