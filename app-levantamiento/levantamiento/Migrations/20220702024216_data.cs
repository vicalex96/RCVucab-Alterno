using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace levantamiento.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    incidenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    polizaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.incidenteId);
                });

            migrationBuilder.CreateTable(
                name: "Partes",
                columns: table => new
                {
                    parteId = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partes", x => x.parteId);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesReparacion",
                columns: table => new
                {
                    SolicitudReparacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    incidenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaSolicitud = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesReparacion", x => x.SolicitudReparacionId);
                    table.ForeignKey(
                        name: "FK_SolicitudesReparacion_Incidentes_incidenteId",
                        column: x => x.incidenteId,
                        principalTable: "Incidentes",
                        principalColumn: "incidenteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requerimientos",
                columns: table => new
                {
                    requerimientoId = table.Column<Guid>(type: "uuid", nullable: false),
                    solicitudReparacionId = table.Column<Guid>(type: "uuid", nullable: false),
                    parteId = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    tipoRequerimiento = table.Column<int>(type: "integer", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requerimientos", x => x.requerimientoId);
                    table.ForeignKey(
                        name: "FK_Requerimientos_Partes_parteId",
                        column: x => x.parteId,
                        principalTable: "Partes",
                        principalColumn: "parteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requerimientos_SolicitudesReparacion_solicitudReparacionId",
                        column: x => x.solicitudReparacionId,
                        principalTable: "SolicitudesReparacion",
                        principalColumn: "SolicitudReparacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "polizaId" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-bc01-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b") },
                    { new Guid("0c5c3262-d5ef-46c7-bc01-97530821c05b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04c") },
                    { new Guid("0c5c3262-d5ef-46c7-bc01-97530821c06b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04d") }
                });

            migrationBuilder.InsertData(
                table: "Partes",
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
                table: "SolicitudesReparacion",
                columns: new[] { "SolicitudReparacionId", "fechaSolicitud", "incidenteId", "tallerId", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"), new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc01-97530821c06b"), new Guid("10003262-d5ef-46c7-bc0e-97530823c05b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b") });

            migrationBuilder.InsertData(
                table: "Requerimientos",
                columns: new[] { "requerimientoId", "cantidad", "descripcion", "parteId", "solicitudReparacionId", "tipoRequerimiento" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-2000-bc0e-97530821c04b"), 1, "cambio capó de la maleta", new Guid("0c5c3262-d5ef-46c7-5000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"), 1 },
                    { new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c0c1"), 1, "puerta detrozada parcialmente", new Guid("0c5c3262-d5ef-46c7-2000-97530821c04b"), new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_parteId",
                table: "Requerimientos",
                column: "parteId");

            migrationBuilder.CreateIndex(
                name: "IX_Requerimientos_solicitudReparacionId",
                table: "Requerimientos",
                column: "solicitudReparacionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesReparacion_incidenteId",
                table: "SolicitudesReparacion",
                column: "incidenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requerimientos");

            migrationBuilder.DropTable(
                name: "Partes");

            migrationBuilder.DropTable(
                name: "SolicitudesReparacion");

            migrationBuilder.DropTable(
                name: "Incidentes");
        }
    }
}
