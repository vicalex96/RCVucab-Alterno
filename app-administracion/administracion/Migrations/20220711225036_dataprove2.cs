using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class dataprove2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("10000000-d5ef-46c7-0004-000000000001"));

            migrationBuilder.InsertData(
                table: "Asegurados",
                columns: new[] { "aseguradoId", "apellido", "nombre" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), "Gimenez", "Daniel" },
                    { new Guid("0c5c3262-d5ef-46c7-0001-000000000004"), "Salaguchi", "Maria Jose" }
                });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0004-000000000001"), 0, null, new DateTime(2010, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000001") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0003-000000000001"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2009, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2014, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Polizas",
                columns: new[] { "polizaId", "fechaRegistro", "fechaVencimiento", "tipoPoliza", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0003-000000000002"), new DateTime(2016, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new Guid("0c5c3262-d5ef-46c7-0002-000000000002") });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "proveedorId", "nombreLocal" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0006-000000000001"), "Todo en partes 3000" },
                    { new Guid("0c5c3262-d5ef-46c7-0006-000000000002"), "Tu Carro, tu repuesto" }
                });

            migrationBuilder.InsertData(
                table: "Talleres",
                columns: new[] { "tallerId", "nombreLocal" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0005-000000000001"), "Gas Monkey" },
                    { new Guid("0c5c3262-d5ef-46c7-0005-000000000002"), "Taller de Luis" }
                });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000001"),
                columns: new[] { "anioModelo", "fechaCompra" },
                values: new object[] { 2007, new DateTime(2007, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000002"),
                column: "fechaCompra",
                value: new DateTime(2014, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0004-000000000002"), 0, null, new DateTime(2018, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000002") });

            migrationBuilder.InsertData(
                table: "MarcasProveedor",
                columns: new[] { "marcaId", "manejaTodas", "marca", "proveedorId" },
                values: new object[,]
                {
                    { new Guid("00000001-d5ef-46c7-0006-000000000001"), false, 8, new Guid("0c5c3262-d5ef-46c7-0006-000000000001") },
                    { new Guid("00000001-d5ef-46c7-0006-000000000002"), true, null, new Guid("0c5c3262-d5ef-46c7-0006-000000000002") },
                    { new Guid("00000002-d5ef-46c7-0006-000000000001"), false, 5, new Guid("0c5c3262-d5ef-46c7-0006-000000000001") },
                    { new Guid("00000003-d5ef-46c7-0006-000000000001"), false, 7, new Guid("0c5c3262-d5ef-46c7-0006-000000000001") },
                    { new Guid("00000004-d5ef-46c7-0006-000000000001"), false, 0, new Guid("0c5c3262-d5ef-46c7-0006-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "MarcasTaller",
                columns: new[] { "marcaId", "manejaTodas", "marca", "tallerId" },
                values: new object[,]
                {
                    { new Guid("00000001-d5ef-46c7-0005-000000000001"), false, 8, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") },
                    { new Guid("00000001-d5ef-46c7-0005-000000000002"), true, null, new Guid("0c5c3262-d5ef-46c7-0005-000000000002") },
                    { new Guid("00000002-d5ef-46c7-0005-000000000001"), false, 4, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") },
                    { new Guid("00000003-d5ef-46c7-0005-000000000001"), false, 7, new Guid("0c5c3262-d5ef-46c7-0005-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "vehiculoId", "anioModelo", "aseguradoId", "color", "fechaCompra", "marca", "placa" },
                values: new object[,]
                {
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000003"), 2016, new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), 5, new DateTime(2017, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "BB322AC" },
                    { new Guid("0c5c3262-d5ef-46c7-0002-000000000004"), 2020, new Guid("0c5c3262-d5ef-46c7-0001-000000000003"), 9, new DateTime(2020, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "BB329AC" }
                });

            migrationBuilder.InsertData(
                table: "Polizas",
                columns: new[] { "polizaId", "fechaRegistro", "fechaVencimiento", "tipoPoliza", "vehiculoId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0003-000000000003"), new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("0c5c3262-d5ef-46c7-0002-000000000003") });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("0c5c3262-d5ef-46c7-0004-000000000003"), 0, null, new DateTime(2021, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000003") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Asegurados",
                keyColumn: "aseguradoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0001-000000000004"));

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0004-000000000001"));

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0004-000000000002"));

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0004-000000000003"));

            migrationBuilder.DeleteData(
                table: "MarcasProveedor",
                keyColumn: "marcaId",
                keyValue: new Guid("00000001-d5ef-46c7-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasProveedor",
                keyColumn: "marcaId",
                keyValue: new Guid("00000001-d5ef-46c7-0006-000000000002"));

            migrationBuilder.DeleteData(
                table: "MarcasProveedor",
                keyColumn: "marcaId",
                keyValue: new Guid("00000002-d5ef-46c7-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasProveedor",
                keyColumn: "marcaId",
                keyValue: new Guid("00000003-d5ef-46c7-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasProveedor",
                keyColumn: "marcaId",
                keyValue: new Guid("00000004-d5ef-46c7-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasTaller",
                keyColumn: "marcaId",
                keyValue: new Guid("00000001-d5ef-46c7-0005-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasTaller",
                keyColumn: "marcaId",
                keyValue: new Guid("00000001-d5ef-46c7-0005-000000000002"));

            migrationBuilder.DeleteData(
                table: "MarcasTaller",
                keyColumn: "marcaId",
                keyValue: new Guid("00000002-d5ef-46c7-0005-000000000001"));

            migrationBuilder.DeleteData(
                table: "MarcasTaller",
                keyColumn: "marcaId",
                keyValue: new Guid("00000003-d5ef-46c7-0005-000000000001"));

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000004"));

            migrationBuilder.DeleteData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0003-000000000002"));

            migrationBuilder.DeleteData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "proveedorId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "Proveedores",
                keyColumn: "proveedorId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0006-000000000002"));

            migrationBuilder.DeleteData(
                table: "Talleres",
                keyColumn: "tallerId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0005-000000000001"));

            migrationBuilder.DeleteData(
                table: "Talleres",
                keyColumn: "tallerId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0005-000000000002"));

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000003"));

            migrationBuilder.DeleteData(
                table: "Asegurados",
                keyColumn: "aseguradoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0001-000000000003"));

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("10000000-d5ef-46c7-0004-000000000001"), 0, null, new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-0003-000000000001") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0003-000000000001"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2020, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000001"),
                columns: new[] { "anioModelo", "fechaCompra" },
                values: new object[] { 2004, new DateTime(2018, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-0002-000000000002"),
                column: "fechaCompra",
                value: new DateTime(2010, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
