using Microsoft.EntityFrameworkCore.Migrations;

namespace DropDownCrud.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityID",
                table: "Cities",
                newName: "DistID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistID",
                table: "Cities",
                newName: "CityID");
        }
    }
}
