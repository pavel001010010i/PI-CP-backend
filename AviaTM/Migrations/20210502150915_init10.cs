using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports");

            migrationBuilder.AlterColumn<int>(
                name: "IdTransLodaCapacity",
                table: "Transports",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "IdTransLoadCapacity",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity",
                principalTable: "TransportLoadCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "IdTransLoadCapacity",
                table: "Transports");

            migrationBuilder.AlterColumn<int>(
                name: "IdTransLodaCapacity",
                table: "Transports",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity",
                principalTable: "TransportLoadCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
