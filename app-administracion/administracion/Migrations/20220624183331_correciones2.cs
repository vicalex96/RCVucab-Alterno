using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class correciones2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcasProveedor_Proveedores_ProveedorId",
                table: "MarcasProveedor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("9a66ccc9-871a-432c-a3b6-972383e404da"));

            migrationBuilder.RenameColumn(
                name: "ProveedorId",
                table: "MarcasProveedor",
                newName: "proveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_MarcasProveedor_ProveedorId",
                table: "MarcasProveedor",
                newName: "IX_MarcasProveedor_proveedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller",
                columns: new[] { "marcaId", "tallerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor",
                columns: new[] { "marcaId", "proveedorId" });

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("9e2ff60b-c41f-4c03-8321-30c5e392c3f4"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 24, 14, 33, 31, 29, DateTimeKind.Local).AddTicks(6360), new DateTime(2022, 6, 24, 14, 33, 31, 29, DateTimeKind.Local).AddTicks(6361) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 14, 33, 31, 29, DateTimeKind.Local).AddTicks(6339));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 14, 33, 31, 29, DateTimeKind.Local).AddTicks(6356));

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId",
                table: "MarcasProveedor",
                column: "proveedorId",
                principalTable: "Proveedores",
                principalColumn: "proveedorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId",
                table: "MarcasProveedor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("9e2ff60b-c41f-4c03-8321-30c5e392c3f4"));

            migrationBuilder.RenameColumn(
                name: "proveedorId",
                table: "MarcasProveedor",
                newName: "ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_MarcasProveedor_proveedorId",
                table: "MarcasProveedor",
                newName: "IX_MarcasProveedor_ProveedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller",
                column: "marcaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor",
                column: "marcaId");

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("9a66ccc9-871a-432c-a3b6-972383e404da"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 24, 14, 27, 37, 138, DateTimeKind.Local).AddTicks(5456), new DateTime(2022, 6, 24, 14, 27, 37, 138, DateTimeKind.Local).AddTicks(5456) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 14, 27, 37, 138, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 14, 27, 37, 138, DateTimeKind.Local).AddTicks(5453));

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasProveedor_Proveedores_ProveedorId",
                table: "MarcasProveedor",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "proveedorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
