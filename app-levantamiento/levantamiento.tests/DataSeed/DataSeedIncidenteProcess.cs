using MockQueryable.Moq;
using Moq;
using levantamiento.Persistence.Database;
using levantamiento.Persistence.Entities;

namespace levantamiento.Test.DataSeed
{
    public static class DataSeedIncidenteProcess
    {
        public static void SetupDbContextDataSolcitudes(this Mock<ILevantamientoDBContext> _mockContext)
        {
            var Partes = new List<Parte>{
                new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-0002-000000000001"),
                nombre = "capó", 
                },
                new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-0002-000000000002"),
                nombre = "Puerta delantera izquierda", 
                },
                new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-0002-000000000003"),
                nombre = "retrovisor derecho", 
                },
            };

            var Incidentes = new List<Incidente>{
                new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000001"),
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000001"),
                solicitudes = new List<SolicitudReparacion>(),
                },
                new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000002"),
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000002"),
                solicitudes = new List<SolicitudReparacion>(),
                },
                new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000003"),
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000003"),
                solicitudes = new List<SolicitudReparacion>(),
                }
            };

            var SolicitudesReparacion = new List<SolicitudReparacion>{
                new SolicitudReparacion()
                {
                    SolicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000000"),
                    incidenteId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000001"),
                    vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001"),
                    tallerId = Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000002"),
                    fechaSolicitud = DateTime.ParseExact("01-07-2010", "dd-MM-yyyy",null),
                    incidente = Incidentes[0],
                    requerimientos = new List<Requerimiento>(),
                },
                new SolicitudReparacion()
                {
                    SolicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000001"),
                    incidenteId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000003"),
                    vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000005"),
                    tallerId = It.IsAny<Guid>(),
                    fechaSolicitud = DateTime.ParseExact("01-07-2020", "dd-MM-yyyy",null),
                    incidente = Incidentes[2],
                    requerimientos = new List<Requerimiento>(),
                }
            };
            Incidentes[0].solicitudes!.Add(SolicitudesReparacion[0]);
            Incidentes[1].solicitudes!.Add(SolicitudesReparacion[1]);

            var Requerimientos = new List<Requerimiento>{
                new Requerimiento{
                    requerimientoId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-000000000001"),
                    solicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000000"),
                    parteId = Guid.Parse( "0c5c3262-d5ef-46c7-0002-000000000001"),
                    descripcion = "puerta detrozada parcialmente",
                    tipoRequerimiento = TipoRequerimiento.Reparacion,
                    cantidad = 1,
                    solicitudReparacion = SolicitudesReparacion[0],
                    parte = Partes[0],
                },
                new Requerimiento{
                    requerimientoId = Guid.Parse("0c5c3262-d5ef-2000-bc0e-000000000002"),
                    solicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000000"),
                    parteId = Guid.Parse( "0c5c3262-d5ef-46c7-0002-000000000002"),
                    descripcion = "la pieza ",
                    tipoRequerimiento = TipoRequerimiento.ComprarPieza,
                    cantidad = 1,
                    solicitudReparacion = SolicitudesReparacion[0],
                    parte = Partes[1],
                },
            };
            SolicitudesReparacion[0].requerimientos!.Add(Requerimientos[0]);
            SolicitudesReparacion[0].requerimientos!.Add(Requerimientos[1]);

            _mockContext.Setup(
                c => c.Partes
                ).Returns(
                    Partes.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Incidentes
                ).Returns(
                    Incidentes.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.SolicitudesReparacion
                ).Returns(
                    SolicitudesReparacion.AsQueryable().BuildMockDbSet().Object
                    );
            _mockContext.Setup(
                c => c.Requerimientos
                ).Returns(
                    Requerimientos.AsQueryable().BuildMockDbSet().Object
                    );
        }

    }
}