using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class correccion_atributo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("6d285dbf-986a-4635-a622-ccdbd214fd01"));

            migrationBuilder.RenameColumn(
                name: "estadoPoliza",
                table: "Incidentes",
                newName: "estadoIncidente");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("9189c937-3d16-447e-a452-457189c9015a"));

            migrationBuilder.RenameColumn(
                name: "estadoIncidente",
                table: "Incidentes",
                newName: "estadoPoliza");

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoPoliza", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("6d285dbf-986a-4635-a622-ccdbd214fd01"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local).AddTicks(5477), new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local).AddTicks(5478) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local).AddTicks(5474));
        }
    }
}
