using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TypeTransports",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TypeCargoes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TypeTransports");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TypeCargoes");
        }
    }
}
