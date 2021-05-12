using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AviaTM.Migrations
{
    public partial class UpdateTypeIdUserInOrderAndRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDataId",
                table: "Cargos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderMain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTransport = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMain_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderMain_Transports_IdTransport",
                        column: x => x.IdTransport,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestMain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTransport = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMain_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestMain_Transports_IdTransport",
                        column: x => x.IdTransport,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUser = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    OrderMainId = table.Column<int>(type: "integer", nullable: true),
                    RequestMainId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderData_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderData_OrderMain_OrderMainId",
                        column: x => x.OrderMainId,
                        principalTable: "OrderMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderData_RequestMain_RequestMainId",
                        column: x => x.RequestMainId,
                        principalTable: "RequestMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_OrderDataId",
                table: "Cargos",
                column: "OrderDataId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_IdUser",
                table: "OrderData",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_OrderMainId",
                table: "OrderData",
                column: "OrderMainId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderData_RequestMainId",
                table: "OrderData",
                column: "RequestMainId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMain_IdTransport",
                table: "OrderMain",
                column: "IdTransport");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMain_IdUser",
                table: "OrderMain",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMain_IdTransport",
                table: "RequestMain",
                column: "IdTransport");

            migrationBuilder.CreateIndex(
                name: "IX_RequestMain_IdUser",
                table: "RequestMain",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_OrderData_OrderDataId",
                table: "Cargos",
                column: "OrderDataId",
                principalTable: "OrderData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_OrderData_OrderDataId",
                table: "Cargos");

            migrationBuilder.DropTable(
                name: "OrderData");

            migrationBuilder.DropTable(
                name: "OrderMain");

            migrationBuilder.DropTable(
                name: "RequestMain");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_OrderDataId",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "OrderDataId",
                table: "Cargos");
        }
    }
}
