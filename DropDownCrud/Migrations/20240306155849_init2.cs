using Microsoft.EntityFrameworkCore.Migrations;

namespace DropDownCrud.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_CountryCID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_CountryCID",
                table: "States");

            migrationBuilder.DropColumn(
                name: "CountryCID",
                table: "States");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountyID",
                table: "States",
                column: "CountyID");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_CountyID",
                table: "States",
                column: "CountyID",
                principalTable: "Countries",
                principalColumn: "CID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Countries_CountyID",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_CountyID",
                table: "States");

            migrationBuilder.AddColumn<int>(
                name: "CountryCID",
                table: "States",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryCID",
                table: "States",
                column: "CountryCID");

            migrationBuilder.AddForeignKey(
                name: "FK_States_Countries_CountryCID",
                table: "States",
                column: "CountryCID",
                principalTable: "Countries",
                principalColumn: "CID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
