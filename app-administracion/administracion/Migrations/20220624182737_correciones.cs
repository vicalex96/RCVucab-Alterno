using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace administracion.Migrations
{
    public partial class correciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "tallerId1",
                table: "MarcasTaller",
                newName: "marcaId");

            migrationBuilder.RenameColumn(
                name: "proveedorId",
                table: "MarcasProveedor",
                newName: "ProveedorId");

            migrationBuilder.RenameColumn(
                name: "proveedorId1",
                table: "MarcasProveedor",
                newName: "marcaId");

            migrationBuilder.AlterColumn<int>(
                name: "marca",
                table: "MarcasTaller",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "marca",
                table: "MarcasProveedor",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.CreateIndex(
                name: "IX_MarcasTaller_tallerId",
                table: "MarcasTaller",
                column: "tallerId");

            migrationBuilder.CreateIndex(
                name: "IX_MarcasProveedor_ProveedorId",
                table: "MarcasProveedor",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcasProveedor_Proveedores_ProveedorId",
                table: "MarcasProveedor",
                column: "ProveedorId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcasProveedor_Proveedores_ProveedorId",
                table: "MarcasProveedor");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcasTaller_Talleres_tallerId",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasTaller",
                table: "MarcasTaller");

            migrationBuilder.DropIndex(
                name: "IX_MarcasTaller_tallerId",
                table: "MarcasTaller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarcasProveedor",
                table: "MarcasProveedor");

            migrationBuilder.DropIndex(
                name: "IX_MarcasProveedor_ProveedorId",
                table: "MarcasProveedor");

            migrationBuilder.DeleteData(
                table: "Incidentes",
                keyColumn: "incidenteId",
                keyValue: new Guid("9a66ccc9-871a-432c-a3b6-972383e404da"));

            migrationBuilder.RenameColumn(
                name: "marcaId",
                table: "MarcasTaller",
                newName: "tallerId1");

            migrationBuilder.RenameColumn(
                name: "ProveedorId",
                table: "MarcasProveedor",
                newName: "proveedorId");

            migrationBuilder.RenameColumn(
                name: "marcaId",
                table: "MarcasProveedor",
                newName: "proveedorId1");

            migrationBuilder.AlterColumn<int>(
                name: "marca",
                table: "MarcasTaller",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "marca",
                table: "MarcasProveedor",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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
    }
}
