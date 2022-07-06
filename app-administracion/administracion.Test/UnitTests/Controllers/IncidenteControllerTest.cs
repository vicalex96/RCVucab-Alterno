using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.BussinesLogic.DTOs;
using administracion.Controllers;
using administracion.Exceptions;
using administracion.Persistence.DAOs;
using administracion.Responses;
using Xunit;

namespace RCVUcab.Test.UnitTests.Controllers
{
    public class IncidenteControllerTest
    {
        private readonly IncidenteController _controller;
        private readonly Mock<IIncidenteDAO> _serviceMock;
        private readonly Mock<ILogger<IncidenteController>> _loggerMock;

        public IncidenteControllerTest()
        {
            _loggerMock = new Mock<ILogger<IncidenteController>>();
            _serviceMock = new Mock<IIncidenteDAO>();
            _controller = new IncidenteController(_loggerMock.Object,
                _serviceMock.Object);
            

            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Registrar Incidente")]
        public Task RegisterIncidente()
        {
            _serviceMock
                .Setup(x => x.RegisterIncidente(It.IsAny<IncidenteRegisterDTO>()))
                .Returns(It.IsAny<bool>());
            
            var result = _controller.RegistrarIncidente(It.IsAny<IncidenteRegisterDTO>());

            Assert.IsType<ApplicationResponse<bool>>(result);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Registrar Incidente regresa una excepcion")]
        public Task RegisterIncidenteException()
        {
            _serviceMock
                .Setup(x => x.RegisterIncidente(It.IsAny<IncidenteRegisterDTO>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.RegistrarIncidente(It.IsAny<IncidenteRegisterDTO>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Consultar inciente por Guid")]
        public Task GetIncidenteByGuid()
        {
            _serviceMock.Setup( x => x.consultarIncidente(It.IsAny<Guid>()))
            .Returns(new IncidenteDTO());
            var result = _controller.consultarIncidente(It.IsAny<Guid>());
            
            Assert.IsType<ApplicationResponse<IncidenteDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Controller: Consultar inciente por Guid retorna una Excepcion")]
        public Task GetIncidenteByGuidException()
        {
            _serviceMock
                .Setup(x => x.consultarIncidente(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.consultarIncidente(It.IsAny<Guid>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener un Incidentes activos")]
        public Task GetActiveIncidentes()
        {
            _serviceMock.Setup( x => x.ConsultarIncidentesActivos())
            .Returns(new List<IncidenteDTO>());
            var result = _controller.ConsultarIncidentesActivos();
            
            Assert.IsType<ApplicationResponse<List<IncidenteDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener un Incidentes activos provoca una excepcion")]
        public Task GetActiveIncidentesException()
        {
            _serviceMock
                .Setup(x => x.ConsultarIncidentesActivos())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.ConsultarIncidentesActivos();

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Actualizar Estado Incidente")]
        public Task UpdateIncidenteState()
        {
            _serviceMock.Setup( x => x.actualizarIncidente(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>()))
            .Returns(It.IsAny<bool>());
            var result = _controller.actualizarIncidente(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>());
            
            Assert.IsType<ApplicationResponse<bool>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Actualizar Estado Incidente arroja excepcion")]
        public Task CreateIncidenteException()
        {
            _serviceMock
                .Setup(x => x.actualizarIncidente(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.actualizarIncidente(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
    }
}
