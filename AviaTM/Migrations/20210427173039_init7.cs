using Microsoft.EntityFrameworkCore.Migrations;

namespace AviaTM.Migrations
{
    public partial class init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTypeCurrency",
                table: "Cargos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCurrency", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_IdTypeCurrency",
                table: "Cargos",
                column: "IdTypeCurrency");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TypeCurrency_IdTypeCurrency",
                table: "Cargos",
                column: "IdTypeCurrency",
                principalTable: "TypeCurrency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TypeCurrency_IdTypeCurrency",
                table: "Cargos");

            migrationBuilder.DropTable(
                name: "TypeCurrency");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_IdTypeCurrency",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "IdTypeCurrency",
                table: "Cargos");
        }
    }
}
