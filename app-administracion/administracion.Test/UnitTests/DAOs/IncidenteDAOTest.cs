using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using Xunit;
using administracion.Persistence.Entities;
using administracion.Exceptions;

namespace administracion.Test.UnitTests.DAOs
{

    public class IncidenteDAOShould
    {
        private readonly IncidenteDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<IncidenteDAO>> _mockLogger;

        public IncidenteDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _mockLogger = new Mock<ILogger<IncidenteDAO>>();

            _dao = new IncidenteDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataIncidenteProcess();
        }

        [Theory(DisplayName = "DAO: Consultar Incidente según su Guid regresar un incidente")]
        [InlineData("000001c9-12aa-46bf-82a2-05ff65bb0000")]
        public Task ShouldGetIncidenteByGuid(Guid id)
        {
            IncidenteDTO incidente = _dao.GetIncidenteById(id);

            Assert.NotNull(incidente);
            return Task.CompletedTask;
        }
        
        [Theory(DisplayName = "DAO: Consulta Incidentes según estado y regresa lista de incientes")]
        [InlineData(EstadoIncidente.Pendiente)]
        public Task ShouldGetIncidentesByStateReturnList(EstadoIncidente estado)
        {
            var result = _dao.GetIncidentesByState(estado);
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "DAO: Registrar un incidente deberia retornar true")]
        public Task ShouldRegisterIncienteReturnTrue()
        {   
            Incidente incidente= new Incidente();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);

            var respuesta = _dao.RegisterIncidente(incidente);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Registrar un incidente deberia retornar un RCVException")]
        public Task ShouldRegisterIncienteReturnException()
        {   
            Incidente incidente= new Incidente();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws( new Exception());
            
            Assert.Throws<RCVException>(() => _dao.RegisterIncidente(incidente));
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "DAO: Actualiza el estado del incidente retorna true")]
        public Task ShouldUpdateIncidenteStateReturnTrue()
        {
            Incidente incidente = new Incidente();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(0);
            bool result = _dao.UpdateIncidente(incidente);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Actualiza el estado del incidente retorna una excepcion")]
        public Task ShouldUpdateIncidenteStateReturnException()
        {
            Incidente incidente = new Incidente();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVUpdateException>(() => _dao.UpdateIncidente(incidente));
            return Task.CompletedTask;
        }   
    
    }
}