using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;
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
            bool result = _dao.actualizarIncidente(incidente);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Actualiza el estado del incidente retorna una excepcion")]
        public Task ShouldUpdateIncidenteStateReturnException()
        {
            Incidente incidente = new Incidente();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _dao.actualizarIncidente(incidente));
            return Task.CompletedTask;
        }   
    
    }
}