
using Microsoft.Extensions.Logging;
using Moq;
using levantamiento.Persistence.DAOs;
using levantamiento.Persistence.Database;
using levantamiento.Test.DataSeed;
using Xunit;
using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections;


namespace administracion.Test.UnitTests.DAOs
{
    public class IncidenteDAOShould
    {
        private readonly IncidenteDAO _dao;
        private readonly Mock<ILevantamientoDBContext> _contextMock;
        private readonly Mock<ILogger<ParteDAO>> _mockLogger;
        public IncidenteDAOShould()
        {
            _contextMock = new Mock<ILevantamientoDBContext>();
            _mockLogger = new Mock<ILogger<ParteDAO>>();

            _dao = new IncidenteDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataSolcitudes();
        }

        public class IncidenteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Incidente()
                    {
                        incidenteId = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000008"),
                        polizaId = Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000250"),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName ="DAO: Registrar incidente, la prueba devuelve un True")]
        [ClassData(typeof(IncidenteClassData))]
        public Task RegisterIncidenteReturnTrue(Incidente incidente)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(0);
            var result = _dao.RegisterIncidente(incidente);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName ="DAO: Solicita la lista de incidentes, retorna True")]
        public Task GetListOfIncidentesReturnTrue()
        {
            var result = _dao.GetAll();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Muestra todos los incidentes a los que aun le falten generar alguna solicitud")]
        public Task GetListOfIncidentesWithoutSolicitudesReturnTrue()
        {
            var result = _dao.GetAllWithoutSolicitud();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consulta un incidente por su Id, devuelve un incidente")]
        [InlineData("0c5c3262-d5ef-46c7-0004-000000000001")]
        public Task GetIncidenteByIdReturnIncidente(Guid incidenteId)
        {
            var result = _dao.GetIncidenteById(incidenteId);
            Assert.NotNull(result);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Intenta consultar un incidente por su Id pero que no existe, devuelve un null")]
        [InlineData("0c5c3262-d5ef-46c7-0002-000000000010")]
        public Task GetIncidenteByIdReturnNull(Guid incidenteId)
        {
            var result = _dao.GetIncidenteById(incidenteId);
            Assert.Null(result);
            return Task.CompletedTask;
        }
    }
}