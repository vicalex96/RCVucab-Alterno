//using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.DAOs
{

    public class IncidenteDAOShould
    {
        private readonly IncidenteDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<IncidenteDAO>> _mockLogger;

        public IncidenteDAOShould()
        {
            //var faker = new Faker();
            _contextMock = new Mock<IAdminDBContext>();
            // el Mock no emplea un DBcontext real en IAdminDBContext =>  obligamos una respuesta por defecto para el SaveChanges y de esta forma evitar un error al no tener un DBcontext real
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<IncidenteDAO>>();

            _dao = new IncidenteDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataVehiculo();
        }

        public class IncidenteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new IncidenteSimpleDTO
                    {
                        incidenteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2100"),
                        polizaId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2100")
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }



        [Theory(DisplayName = "DAO: Registrar un incidente deberia retornar un mensaje")]
        [ClassData(typeof(IncidenteClassData))]
        public Task ShouldRegisterInciente(IncidenteSimpleDTO incidente)
        {
            var respuesta = _dao.RegisterIncidente(incidente);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar Incidente según su Guid regresar un incidente")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2100")]
        public Task ShouldGetIncidenteByGuid(Guid id)
        {
            IncidenteDTO incidente = _dao.consultarIncidente(id);

            Assert.NotNull(incidente);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "DAO: Consulta Incidentes Activos y regresa lista")]
        public Task ShouldGetActiveIncidentes()
        {
            //Arrage
            var result = _dao.ConsultarIncidentesActivos();
            //Act
            var isNoEmpty = result.Any();
            //Assert
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }


    }
}