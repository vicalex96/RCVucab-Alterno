
using Microsoft.Extensions.Logging;
using Moq;
using levantamiento.DataAccess.DAOs;
using levantamiento.DataAccess.Database;
using levantamiento.Test.DataSeed;
using Xunit;
using levantamiento.Exceptions;
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections;


namespace administracion.Test.UnitTests.DAOs
{
    public class SolicitudDAOShould
    {
        private readonly SolcitudReparacionDAO _dao;
        private readonly Mock<ILevantamientoDBContext> _contextMock;
        private readonly Mock<ILogger<ParteDAO>> _mockLogger;
        public SolicitudDAOShould()
        {
            _contextMock = new Mock<ILevantamientoDBContext>();
            _mockLogger = new Mock<ILogger<ParteDAO>>();

            _dao = new SolcitudReparacionDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataSolcitudes();
        }
        [Fact(DisplayName = "DAO: Consultar toda la lista de solicitudes en el sistema, la prueba decuelve un True")]
        public Task GetListOfSolicitudesReturnTrue()
        {
            var result = _dao.GetAll();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Solicita consultar aquellas  solicitudes sin taller asociacia retorna True")]
        public Task GetListOfSolicitudesWhithoutTallerReturnTrue()
        {
            var result = _dao.GetSolicitudWithoutTaller();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consulta una solicitud segun su Id, retorna Solicitud")]
        [InlineData("0c5c3262-d5ef-46c7-0001-000000000000")]
        public Task GetSolicitudByIdReturnSolicitud(Guid solicitudId)
        {
            SolicitudesReparacionDTO result = _dao.GetSolicitudById(solicitudId);
            Assert.NotNull(result);
            return Task.CompletedTask;
        } 

        [Theory(DisplayName = "DAO: Consulta una solicitud segun su Id, retorna null")]
        [InlineData("0c5c3262-d5ef-46c7-0001-000000000002")]
        public Task GetSolicitudByIdReturnNull(Guid solicitudId)
        {
            SolicitudesReparacionDTO result = _dao.GetSolicitudById(solicitudId);
            Assert.Null(result);
            return Task.CompletedTask;
        } 

        [Theory(DisplayName = "DAO: Consulta solicitudes segun el Id de Incidente, retorna Solicitud")]
        [InlineData("0c5c3262-d5ef-46c7-0004-000000000001")]
        public Task GetSolicitudByIncidenteIdReturnSolicitud(Guid incidenteId)
        {
            List<SolicitudesReparacionDTO> result = _dao.GetSolicitudByIncidenteId(incidenteId);
            bool isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        } 

        [Theory(DisplayName = "DAO: Consulta solicitudes segun el Id de Incidente, retorna null")]
        [InlineData("0c5c3262-d5ef-46c7-0004-000000002000")]
        public Task GetSolicitudByIncidenteIdReturnNull(Guid incidenteId)
        {
            List<SolicitudesReparacionDTO> result = _dao.GetSolicitudByIncidenteId(incidenteId);
            bool isNoEmpty = result.Any();
            Assert.False(isNoEmpty);
            return Task.CompletedTask;
        } 

        public class SolicitudClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new SolicitudReparacion()
                    {
                        SolicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000003"),
                        incidenteId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000007"),
                        vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000010"),
                        tallerId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000004"),
                        fechaSolicitud =DateTime.ParseExact("12-07-2022","dd-mm-yyyy",null)
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "DAO: Registra una parte de vehiculo al listdo del sistema, la prueba devuelve un True")]
        [ClassData(typeof(SolicitudClassData))]
        public Task RegisterParteReturnTrue(SolicitudReparacion solicitud)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(0);
            var result = _dao.RegisterSolicitud(solicitud);
            Assert.True(result);
            return Task.CompletedTask;
        }
    }
}