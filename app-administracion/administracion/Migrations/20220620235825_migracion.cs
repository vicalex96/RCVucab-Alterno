using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class migracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asegurados",
                columns: table => new
                {
                    aseguradoId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asegurados", x => x.aseguradoId);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    anioModelo = table.Column<int>(type: "integer", nullable: false),
                    fechaCompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    color = table.Column<int>(type: "integer", nullable: false),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: false),
                    aseguradoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.vehiculoId);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Asegurados_aseguradoId",
                        column: x => x.aseguradoId,
                        principalTable: "Asegurados",
                        principalColumn: "aseguradoId");
                });

            migrationBuilder.CreateTable(
                name: "Polizas",
                columns: table => new
                {
                    polizaId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tipoPoliza = table.Column<int>(type: "integer", nullable: false),
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polizas", x => x.polizaId);
                    table.ForeignKey(
                        name: "FK_Polizas_Vehiculos_vehiculoId",
                        column: x => x.vehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "vehiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    incidenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    polizaId = table.Column<Guid>(type: "uuid", nullable: false),
                    estadoPoliza = table.Column<int>(type: "integer", nullable: false),
                    fechaRegistrado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fechaFinalizado = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.incidenteId);
                    table.ForeignKey(
                        name: "FK_Incidentes_Polizas_polizaId",
                        column: x => x.polizaId,
                        principalTable: "Polizas",
                        principalColumn: "polizaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Asegurados",
                columns: new[] { "aseguradoId", "apellido", "nombre" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03b"), "Ramirez Gimenez", "Luis Jose" },
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03f"), "Banderas Lopez", "Manuel Diego" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "vehiculoId", "anioModelo", "aseguradoId", "color", "fechaCompra", "marca", "placa" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 2004, new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03b"), 1, new DateTime(2022, 6, 20, 19, 58, 25, 738, DateTimeKind.Local).AddTicks(2170), 0, "AB320AM" },
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"), 2006, new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03f"), 6, new DateTime(2022, 6, 20, 19, 58, 25, 738, DateTimeKind.Local).AddTicks(2183), 0, "AB322AM" }
                });

            migrationBuilder.InsertData(
                table: "Polizas",
                columns: new[] { "polizaId", "fechaRegistro", "fechaVencimiento", "tipoPoliza", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"), new DateTime(2022, 6, 20, 19, 58, 25, 738, DateTimeKind.Local).AddTicks(2186), new DateTime(2022, 6, 20, 19, 58, 25, 738, DateTimeKind.Local).AddTicks(2186), 0, new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b") });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoPoliza", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("14cedda7-08e9-4907-9699-50cd99576c35"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_polizaId",
                table: "Incidentes",
                column: "polizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Polizas_vehiculoId",
                table: "Polizas",
                column: "vehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_aseguradoId",
                table: "Vehiculos",
                column: "aseguradoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidentes");

            migrationBuilder.DropTable(
                name: "Polizas");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Asegurados");
        }
    }
}
