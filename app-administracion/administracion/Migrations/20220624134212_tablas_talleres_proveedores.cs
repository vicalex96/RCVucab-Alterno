using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class tablas_talleres_proveedores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("9189c937-3d16-447e-a452-457189c9015a"));

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    proveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombreLocal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.proveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Talleres",
                columns: table => new
                {
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombreLocal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talleres", x => x.tallerId);
                });

            migrationBuilder.CreateTable(
                name: "MarcasProveedor",
                columns: table => new
                {
                    proveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasProveedor", x => new { x.proveedorId, x.marca });
                    table.ForeignKey(
                        name: "FK_MarcasProveedor_Proveedores_proveedorId",
                        column: x => x.proveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "proveedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarcasTaller",
                columns: table => new
                {
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasTaller", x => new { x.tallerId, x.marca });
                    table.ForeignKey(
                        name: "FK_MarcasTaller_Talleres_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Talleres",
                        principalColumn: "tallerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("6d035115-13e0-425f-8efe-1e061a049ec1"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 24, 9, 42, 12, 257, DateTimeKind.Local).AddTicks(4790), new DateTime(2022, 6, 24, 9, 42, 12, 257, DateTimeKind.Local).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 9, 42, 12, 257, DateTimeKind.Local).AddTicks(4777));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 9, 42, 12, 257, DateTimeKind.Local).AddTicks(4787));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcasProveedor");

            migrationBuilder.DropTable(
                name: "MarcasTaller");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Talleres");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("6d035115-13e0-425f-8efe-1e061a049ec1"));

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("9189c937-3d16-447e-a452-457189c9015a"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 23, 15, 49, 44, 428, DateTimeKind.Local).AddTicks(8686), new DateTime(2022, 6, 23, 15, 49, 44, 428, DateTimeKind.Local).AddTicks(8687) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 23, 15, 49, 44, 428, DateTimeKind.Local).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 23, 15, 49, 44, 428, DateTimeKind.Local).AddTicks(8685));
        }
    }
}
