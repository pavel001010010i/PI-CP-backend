using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Transports",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "minValue",
                table: "TransportLoadCapacity",
                newName: "MinValue");

            migrationBuilder.RenameColumn(
                name: "maxValue",
                table: "TransportLoadCapacity",
                newName: "MaxValue");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "TransportLoadCapacity",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Transports",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "MinValue",
                table: "TransportLoadCapacity",
                newName: "minValue");

            migrationBuilder.RenameColumn(
                name: "MaxValue",
                table: "TransportLoadCapacity",
                newName: "maxValue");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "TransportLoadCapacity",
                newName: "isActive");
        }
    }
}
