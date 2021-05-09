using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsoCountryTo",
                table: "RouteMaps",
                newName: "CountryCodeTo");

            migrationBuilder.RenameColumn(
                name: "IsoCountryFrom",
                table: "RouteMaps",
                newName: "CountryCodeFrom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountryCodeTo",
                table: "RouteMaps",
                newName: "IsoCountryTo");

            migrationBuilder.RenameColumn(
                name: "CountryCodeFrom",
                table: "RouteMaps",
                newName: "IsoCountryFrom");
        }
    }
}
