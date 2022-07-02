using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proveedor.Migrations
{
    public partial class migracionPrueba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CotizacionPartes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CotizacionParteId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrecioParteUnidad = table.Column<float>(type: "real", nullable: false),
                    Requerimientos = table.Column<List<Guid>>(type: "uuid[]", nullable: true),
                    FechaEntrega = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    estado = table.Column<int>(type: "integer", nullable: false),
                    RequerimientoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CotizacionPartes", x => x.Id);
                });

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
                    cotizaciones = table.Column<Guid>(type: "uuid", nullable: false),
                    CotizacionParteEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    SolicitudReparacionsolicitudRepId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.requerimientoId);
                    table.ForeignKey(
                        name: "FK_Requerimientos_CotizacionPartes_CotizacionParteEntityId",
                        column: x => x.CotizacionParteEntityId,
                        principalTable: "CotizacionPartes",
                        principalColumn: "Id");
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

            migrationBuilder.InsertData(
                table: "SolicitudReparacions",
                columns: new[] { "solicitudRepId", "fechaSolicitud", "incidenteId", "tallerId", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), new DateTime(2022, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-d5ef-46c7-bc0e-97530823c05b"), new Guid("10003262-d5ef-46c7-bc0e-97530823c05b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b") });

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
                table: "Requerimientos",
                columns: new[] { "requerimientoId", "CotizacionParteEntityId", "SolicitudReparacionsolicitudRepId", "cantidad", "cotizaciones", "descripcion", "estado", "parteId", "solicitudRepId", "tipoRequerimiento" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-1000-bc0e-97530821c04b"), null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), "puerta detrozada parcialmente", 0, new Guid("0c5c3262-d5ef-46c7-2000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 0 },
                    { new Guid("0c5c3262-d5ef-2000-bc0e-97530821c04b"), null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), "cambio capó de la maleta", 0, new Guid("0c5c3262-d5ef-46c7-5000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_CotizacionParteEntityId",
                table: "Requerimientos",
                column: "CotizacionParteEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_parteId",
                table: "Requerimientos",
                column: "parteId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_SolicitudReparacionsolicitudRepId",
                table: "Requerimientos",
                column: "SolicitudReparacionsolicitudRepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropTable(
                name: "CotizacionPartes");

            migrationBuilder.DropTable(
                name: "partes");

            migrationBuilder.DropTable(
                name: "SolicitudReparacions");
        }
    }
}
