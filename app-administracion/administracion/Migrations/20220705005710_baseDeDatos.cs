using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class baseDeDatos : Migration
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
                name: "Vehiculos",
                columns: table => new
                {
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false),
                    anioModelo = table.Column<int>(type: "integer", nullable: false),
                    fechaCompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    color = table.Column<int>(type: "integer", nullable: false),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
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
                name: "MarcasProveedor",
                columns: table => new
                {
                    marcaId = table.Column<Guid>(type: "uuid", nullable: false),
                    proveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasProveedor", x => x.marcaId);
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
                    marcaId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasTaller", x => x.marcaId);
                    table.ForeignKey(
                        name: "FK_MarcasTaller_Talleres_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Talleres",
                        principalColumn: "tallerId",
                        onDelete: ReferentialAction.Cascade);
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
                    estadoIncidente = table.Column<int>(type: "integer", nullable: false),
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
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000001"), "Ramirez Gimenez", "Luis Jose" },
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000002"), "Banderas Lopez", "Manuel Diego" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "vehiculoId", "anioModelo", "aseguradoId", "color", "fechaCompra", "marca", "placa" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000001"), 2004, new Guid("0c5c3262-d5ef-46c7-0001-000000000001"), 1, new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "AB320AM" },
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000002"), 2006, new Guid("0c5c3262-d5ef-46c7-0001-000000000002"), 6, new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "AB322AM" }
                });

            migrationBuilder.InsertData(
                table: "Polizas",
                columns: new[] { "polizaId", "fechaRegistro", "fechaVencimiento", "tipoPoliza", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0003-000000000001"), new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("0c5c3262-d5ef-46c7-0002-000000000001") });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("10000000-d5ef-46c7-0004-000000000001"), 0, null, new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000001") });

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_polizaId",
                table: "Incidentes",
                column: "polizaId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasProveedor_proveedorId",
                table: "MarcasProveedor",
                column: "proveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasTaller_tallerId",
                table: "MarcasTaller",
                column: "tallerId");

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
                name: "MarcasProveedor");

            migrationBuilder.DropTable(
                name: "MarcasTaller");

            migrationBuilder.DropTable(
                name: "Polizas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Talleres");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Asegurados");
        }
    }
}
