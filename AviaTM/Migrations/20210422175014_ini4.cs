using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class ini4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypeCargoes_Cargos_CargoId",
                table: "TypeCargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeCargoes_TypeTransports_TypeTransportId",
                table: "TypeCargoes");

            migrationBuilder.DropIndex(
                name: "IX_TypeCargoes_CargoId",
                table: "TypeCargoes");

            migrationBuilder.DropIndex(
                name: "IX_TypeCargoes_TypeTransportId",
                table: "TypeCargoes");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "TypeCargoes");

            migrationBuilder.DropColumn(
                name: "TypeTransportId",
                table: "TypeCargoes");

            migrationBuilder.CreateTable(
                name: "CargoTypeCargo",
                columns: table => new
                {
                    CargosId = table.Column<int>(type: "integer", nullable: false),
                    TypeCargoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoTypeCargo", x => new { x.CargosId, x.TypeCargoId });
                    table.ForeignKey(
                        name: "FK_CargoTypeCargo_Cargos_CargosId",
                        column: x => x.CargosId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoTypeCargo_TypeCargoes_TypeCargoId",
                        column: x => x.TypeCargoId,
                        principalTable: "TypeCargoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeCargoTypeTransport",
                columns: table => new
                {
                    TypeCargosId = table.Column<int>(type: "integer", nullable: false),
                    TypeTransportsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCargoTypeTransport", x => new { x.TypeCargosId, x.TypeTransportsId });
                    table.ForeignKey(
                        name: "FK_TypeCargoTypeTransport_TypeCargoes_TypeCargosId",
                        column: x => x.TypeCargosId,
                        principalTable: "TypeCargoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeCargoTypeTransport_TypeTransports_TypeTransportsId",
                        column: x => x.TypeTransportsId,
                        principalTable: "TypeTransports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoTypeCargo_TypeCargoId",
                table: "CargoTypeCargo",
                column: "TypeCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCargoTypeTransport_TypeTransportsId",
                table: "TypeCargoTypeTransport",
                column: "TypeTransportsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoTypeCargo");

            migrationBuilder.DropTable(
                name: "TypeCargoTypeTransport");

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "TypeCargoes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeTransportId",
                table: "TypeCargoes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypeCargoes_CargoId",
                table: "TypeCargoes",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCargoes_TypeTransportId",
                table: "TypeCargoes",
                column: "TypeTransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypeCargoes_Cargos_CargoId",
                table: "TypeCargoes",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeCargoes_TypeTransports_TypeTransportId",
                table: "TypeCargoes",
                column: "TypeTransportId",
                principalTable: "TypeTransports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
