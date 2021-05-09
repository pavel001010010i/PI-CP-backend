using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class updateTransTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                   

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeTransports",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Transports_IdTransLoadCapacity",
                table: "Transports",
                column: "IdTransLoadCapacity");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLoadCapacity",
                table: "Transports",
                column: "IdTransLoadCapacity",
                principalTable: "TransportLoadCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TypeTransports",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTransLodaCapacity",
                table: "Transports",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transports_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_TransportLoadCapacity_IdTransLodaCapacity",
                table: "Transports",
                column: "IdTransLodaCapacity",
                principalTable: "TransportLoadCapacity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
