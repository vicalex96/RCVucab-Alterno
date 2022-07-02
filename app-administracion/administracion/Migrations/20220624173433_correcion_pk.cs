using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class correcion_pk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId",
                table: "MarcasProveedor");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcasTaller_Talleres_tallerId",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("6d035115-13e0-425f-8efe-1e061a049ec1"));

            migrationBuilder.AddColumn<Guid>(
                name: "tallerId1",
                table: "MarcasTaller",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "proveedorId1",
                table: "MarcasProveedor",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller",
                column: "tallerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor",
                column: "proveedorId");

            migrationBuilder.InsertData(
                table: "Incidentes",
                columns: new[] { "incidenteId", "estadoIncidente", "fechaFinalizado", "fechaRegistrado", "polizaId" },
                values: new object[] { new Guid("5c7c3255-619c-46b1-98da-68b4c891aefd"), 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b") });

            migrationBuilder.UpdateData(
                table: "Polizas",
                keyColumn: "polizaId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                columns: new[] { "fechaRegistro", "fechaVencimiento" },
                values: new object[] { new DateTime(2022, 6, 24, 13, 34, 32, 633, DateTimeKind.Local).AddTicks(1492), new DateTime(2022, 6, 24, 13, 34, 32, 633, DateTimeKind.Local).AddTicks(1493) });

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 13, 34, 32, 633, DateTimeKind.Local).AddTicks(1478));

            migrationBuilder.UpdateData(
                table: "Vehiculos",
                keyColumn: "vehiculoId",
                keyValue: new Guid("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                column: "fechaCompra",
                value: new DateTime(2022, 6, 24, 13, 34, 32, 633, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.CreateIndex(
                name: "IX_MarcasTaller_tallerId1",
                table: "MarcasTaller",
                column: "tallerId1");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasProveedor_proveedorId1",
                table: "MarcasProveedor",
                column: "proveedorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId1",
                table: "MarcasProveedor",
                column: "proveedorId1",
                principalTable: "Proveedores",
                principalColumn: "proveedorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasTaller_Talleres_tallerId1",
                table: "MarcasTaller",
                column: "tallerId1",
                principalTable: "Talleres",
                principalColumn: "tallerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId1",
                table: "MarcasProveedor");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcasTaller_Talleres_tallerId1",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller");

            migrationBuilder.DropIndex(
                name: "IX_MarcasTaller_tallerId1",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor");

            migrationBuilder.DropIndex(
                name: "IX_MarcasProveedor_proveedorId1",
                table: "MarcasProveedor");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("5c7c3255-619c-46b1-98da-68b4c891aefd"));

            migrationBuilder.DropColumn(
                name: "tallerId1",
                table: "MarcasTaller");

            migrationBuilder.DropColumn(
                name: "proveedorId1",
                table: "MarcasProveedor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller",
                columns: new[] { "tallerId", "marca" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor",
                columns: new[] { "proveedorId", "marca" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasProveedor_Proveedores_proveedorId",
                table: "MarcasProveedor",
                column: "proveedorId",
                principalTable: "Proveedores",
                principalColumn: "proveedorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasTaller_Talleres_tallerId",
                table: "MarcasTaller",
                column: "tallerId",
                principalTable: "Talleres",
                principalColumn: "tallerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
