using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class updateTransMaxLoadCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AxlePressure",
                table: "Transports",
                newName: "MaxLoadCapacity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxLoadCapacity",
                table: "Transports",
                newName: "AxlePressure");
        }
    }
}
