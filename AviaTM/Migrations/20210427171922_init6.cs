using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdTypeFuel",
                table: "Transports");

            migrationBuilder.AddColumn<int>(
                name: "CostDelivery",
                table: "Cargos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTypePayment",
                table: "Cargos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypePayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePayment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_IdTypePayment",
                table: "Cargos",
                column: "IdTypePayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TypePayment_IdTypePayment",
                table: "Cargos",
                column: "IdTypePayment",
                principalTable: "TypePayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TypePayment_IdTypePayment",
                table: "Cargos");

            migrationBuilder.DropTable(
                name: "TypePayment");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_IdTypePayment",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "CostDelivery",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "IdTypePayment",
                table: "Cargos");

            migrationBuilder.AddColumn<string>(
                name: "IdTypeFuel",
                table: "Transports",
                type: "text",
                nullable: true);
        }
    }
}
