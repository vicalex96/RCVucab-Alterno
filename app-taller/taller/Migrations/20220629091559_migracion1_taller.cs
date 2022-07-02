using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taller.Migrations
{
    public partial class migracion1_taller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partes",
                columns: table => new
                {
                    parteId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partes", x => x.parteId);
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
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    marcaId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.marcaId);
                    table.ForeignKey(
                        name: "FK_Marcas_Talleres_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Talleres",
                        principalColumn: "tallerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudReparacions",
                columns: table => new
                {
                    solicitudRepId = table.Column<Guid>(type: "uuid", nullable: false),
                    incidenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaSolicitud = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudReparacions", x => x.solicitudRepId);
                    table.ForeignKey(
                        name: "FK_SolicitudReparacions_Talleres_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Talleres",
                        principalColumn: "tallerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requerimientos",
                columns: table => new
                {
                    requerimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    solicitudRepId = table.Column<Guid>(type: "uuid", nullable: false),
                    parteId = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    tipoRequerimiento = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false),
                    estado = table.Column<int>(type: "integer", nullable: true),
                    SolicitudReparacionsolicitudRepId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.requerimientoId);
                    table.ForeignKey(
                        name: "FK_Requerimientos_partes_parteId",
                        column: x => x.parteId,
                        principalTable: "partes",
                        principalColumn: "parteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requerimientos_SolicitudReparacions_SolicitudReparacionsoli~",
                        column: x => x.SolicitudReparacionsolicitudRepId,
                        principalTable: "SolicitudReparacions",
                        principalColumn: "solicitudRepId");
                });

            migrationBuilder.CreateTable(
                name: "CotizacionReparaciones",
                columns: table => new
                {
                    cotizacionRepId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    costoManoObra = table.Column<float>(type: "real", nullable: false),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    fechaInicioReparacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fechaFinReparacion = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    solicitudRepId = table.Column<Guid>(type: "uuid", nullable: false),
                    requerimientoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotizacionReparaciones", x => x.cotizacionRepId);
                    table.ForeignKey(
                        name: "FK_CotizacionReparaciones_Requerimientos_requerimientoId",
                        column: x => x.requerimientoId,
                        principalTable: "Requerimientos",
                        principalColumn: "requerimientoId");
                    table.ForeignKey(
                        name: "FK_CotizacionReparaciones_SolicitudReparacions_solicitudRepId",
                        column: x => x.solicitudRepId,
                        principalTable: "SolicitudReparacions",
                        principalColumn: "solicitudRepId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Talleres",
                columns: new[] { "tallerId", "nombreLocal" },
                values: new object[,]
                {
                    { new Guid("10003262-d5ef-46c7-bc0e-97530823c05b"), "Taller de Pepito" },
                    { new Guid("20003262-d5ef-46c7-bc0e-97530823c05b"), "MadMonkey" },
                    { new Guid("30003262-d5ef-46c7-bc0e-97530823c05b"), "Locos Por los Autos" },
                    { new Guid("40003262-d5ef-46c7-bc0e-97530823c05b"), "Roller Customs" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "vehiculoId", "anioModelo", "aseguradoId", "color", "fechaCompra", "marca", "placa" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 2004, new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03b"), 1, new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "AB320AM" },
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"), 2006, new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c03f"), 6, new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "AB322AM" }
                });

            migrationBuilder.InsertData(
                table: "partes",
                columns: new[] { "parteId", "nombre" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-1000-97530821c04b"), "puerta izquierda delantera" },
                    { new Guid("0c5c3262-d5ef-46c7-2000-97530821c04b"), "puerta derecha delantera" },
                    { new Guid("0c5c3262-d5ef-46c7-3000-97530821c04b"), "rueda" },
                    { new Guid("0c5c3262-d5ef-46c7-4000-97530821c04b"), "capó de motor" },
                    { new Guid("0c5c3262-d5ef-46c7-5000-97530821c04b"), "capó de maleta" },
                    { new Guid("0c5c3262-d5ef-46c7-7000-97530821c04b"), "rin" }
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "marcaId", "manejaTodas", "marca", "tallerId" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-15ef-46c7-bc0e-97530821c04b"), false, 0, new Guid("10003262-d5ef-46c7-bc0e-97530823c05b") },
                    { new Guid("0c5c3262-25ef-46c7-bc0e-97530821c04b"), false, 1, new Guid("10003262-d5ef-46c7-bc0e-97530823c05b") },
                    { new Guid("0c5c3262-35ef-46c7-bc0e-97530821c04b"), false, 7, new Guid("20003262-d5ef-46c7-bc0e-97530823c05b") },
                    { new Guid("0c5c3262-45ef-46c7-bc0e-97530821c04b"), false, 2, new Guid("20003262-d5ef-46c7-bc0e-97530823c05b") },
                    { new Guid("0c5c3262-55ef-46c7-bc0e-97530821c04b"), false, 5, new Guid("30003262-d5ef-46c7-bc0e-97530823c05b") },
                    { new Guid("0c5c3262-65ef-46c7-bc0e-97530821c04b"), false, 6, new Guid("30003262-d5ef-46c7-bc0e-97530823c05b") }
                });

            migrationBuilder.InsertData(
                table: "Requerimientos",
                columns: new[] { "requerimientoId", "SolicitudReparacionsolicitudRepId", "cantidad", "descripcion", "estado", "parteId", "solicitudRepId", "tipoRequerimiento" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-1000-bc0e-97530821c04b"), null, 1, "puerta detrozada parcialmente", 0, new Guid("0c5c3262-d5ef-46c7-2000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 0 },
                    { new Guid("0c5c3262-d5ef-2000-bc0e-97530821c04b"), null, 1, "cambio capó de la maleta", 0, new Guid("0c5c3262-d5ef-46c7-5000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 1 }
                });

            migrationBuilder.InsertData(
                table: "SolicitudReparacions",
                columns: new[] { "solicitudRepId", "fechaSolicitud", "incidenteId", "tallerId", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), new DateTime(2022, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), new Guid("10000000-d5ef-46c7-bc0e-97530823c05b"), new Guid("10003262-d5ef-46c7-bc0e-97530823c05b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b") });

            migrationBuilder.CreateIndex(
                name: "IX_CotizacionReparaciones_requerimientoId",
                table: "CotizacionReparaciones",
                column: "requerimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_CotizacionReparaciones_solicitudRepId",
                table: "CotizacionReparaciones",
                column: "solicitudRepId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_tallerId",
                table: "Marcas",
                column: "tallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_parteId",
                table: "Requerimientos",
                column: "parteId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_SolicitudReparacionsolicitudRepId",
                table: "Requerimientos",
                column: "SolicitudReparacionsolicitudRepId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudReparacions_tallerId",
                table: "SolicitudReparacions",
                column: "tallerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CotizacionReparaciones");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropTable(
                name: "partes");

            migrationBuilder.DropTable(
                name: "SolicitudReparacions");

            migrationBuilder.DropTable(
                name: "Talleres");
        }
    }
}
