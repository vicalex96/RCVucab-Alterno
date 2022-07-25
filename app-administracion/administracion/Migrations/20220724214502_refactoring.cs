using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asegurados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asegurados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombreLocal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talleres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombreLocal = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talleres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    anioModelo = table.Column<int>(type: "integer", nullable: false),
                    fechaCompra = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    color = table.Column<int>(type: "integer", nullable: false),
                    placa = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    marca = table.Column<int>(type: "integer", nullable: false),
                    aseguradoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Asegurados_aseguradoId",
                        column: x => x.aseguradoId,
                        principalTable: "Asegurados",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarcasProveedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    proveedorId = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: true),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasProveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcasProveedor_Proveedores_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Proveedores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarcasTaller",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tallerId = table.Column<Guid>(type: "uuid", nullable: false),
                    manejaTodas = table.Column<bool>(type: "boolean", nullable: false),
                    marca = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasTaller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarcasTaller_Talleres_tallerId",
                        column: x => x.tallerId,
                        principalTable: "Talleres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polizas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    tipoPoliza = table.Column<int>(type: "integer", nullable: false),
                    vehiculoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polizas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polizas_Vehiculos_vehiculoId",
                        column: x => x.vehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    polizaId = table.Column<Guid>(type: "uuid", nullable: false),
                    estadoIncidente = table.Column<int>(type: "integer", nullable: false),
                    fechaRegistrado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    fechaFinalizado = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidentes_Polizas_polizaId",
                        column: x => x.polizaId,
                        principalTable: "Polizas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Asegurados",
                columns: new[] { "Id", "apellido", "nombre" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000001"), "Ramirez Gimenez", "Luis Jose" },
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000002"), "Banderas Lopez", "Manuel Diego" },
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), "Gimenez", "Daniel" },
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000004"), "Salaguchi", "Maria Jose" }
                });

            migrationBuilder.InsertData(
                table: "MarcasProveedor",
                columns: new[] { "Id", "manejaTodas", "marca", "proveedorId", "tallerId" },
                values: new object[,]
                {
                    { new Guid("00000001-d5ef-46c7-0006-000000000001"), false, 8, new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), null },
                    { new Guid("00000001-d5ef-46c7-0006-000000000002"), true, null, new Guid("0c5c3262-d5ef-46c7-0006-000000000002"), null },
                    { new Guid("00000002-d5ef-46c7-0006-000000000001"), false, 5, new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), null },
                    { new Guid("00000003-d5ef-46c7-0006-000000000001"), false, 7, new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), null },
                    { new Guid("00000004-d5ef-46c7-0006-000000000001"), false, 0, new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), null }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "nombreLocal" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), "Todo en partes 3000" },
                    { new Guid("0c5c3262-d5ef-46c7-0006-000000000002"), "Tu Carro, tu repuesto" }
                });

            migrationBuilder.InsertData(
                table: "Talleres",
                columns: new[] { "Id", "nombreLocal" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0005-000000000001"), "Gas Monkey" },
                    { new Guid("0c5c3262-d5ef-46c7-0005-000000000002"), "Taller de Luis" }
                });

            migrationBuilder.InsertData(
                table: "MarcasTaller",
                columns: new[] { "Id", "manejaTodas", "marca", "tallerId" },
                values: new object[,]
                {
                    { new Guid("00000001-d5ef-46c7-0005-000000000001"), false, 8, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") },
                    { new Guid("00000001-d5ef-46c7-0005-000000000002"), true, null, new Guid("0c5c3262-d5ef-46c7-0005-000000000002") },
                    { new Guid("00000002-d5ef-46c7-0005-000000000001"), false, 4, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") },
                    { new Guid("00000003-d5ef-46c7-0005-000000000001"), false, 7, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id", "anioModelo", "aseguradoId", "color", "fechaCompra", "marca", "placa" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000001"), 2007, new Guid("0c5c3262-d5ef-46c7-0001-000000000001"), 1, new DateTime(2007, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "AB320AM" },
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000002"), 2006, new Guid("0c5c3262-d5ef-46c7-0001-000000000002"), 6, new DateTime(2014, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "AB322AM" },
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000003"), 2016, new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), 5, new DateTime(2017, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "BB322AC" },
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000004"), 2020, new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), 9, new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "BB329AC" }
                });

            migrationBuilder.InsertData(
                table: "Polizas",
                columns: new[] { "Id", "fechaRegistro", "fechaVencimiento", "tipoPoliza", "vehiculoId" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0003-000000000001"), new DateTime(2009, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("0c5c3262-d5ef-46c7-0002-000000000001") },
                    { new Guid("0c5c3262-d5ef-46c7-0003-000000000002"), new DateTime(2016, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("0c5c3262-d5ef-46c7-0002-000000000002") },
                    { new Guid("0c5c3262-d5ef-46c7-0003-000000000003"), new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("0c5c3262-d5ef-46c7-0002-000000000003") }
                });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "Id", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0004-000000000001"), 0, null, new DateTime(2010, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000001") },
                    { new Guid("0c5c3262-d5ef-46c7-0004-000000000002"), 0, null, new DateTime(2018, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000002") },
                    { new Guid("0c5c3262-d5ef-46c7-0004-000000000003"), 0, null, new DateTime(2021, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000003") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidentes_polizaId",
                table: "Incidentes",
                column: "polizaId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasProveedor_tallerId",
                table: "MarcasProveedor",
                column: "tallerId");

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
