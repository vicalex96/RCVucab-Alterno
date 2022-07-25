using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Controllers;
using levantamiento.Exceptions;
using levantamiento.DataAccess.DAOs;
using levantamiento.Responses;
using Xunit;
using levantamiento.BussinesLogic.Logic;

namespace administracion.Test.UnitTests.Controllers
{
    public class IncidenteControllerTest
    {
        private readonly IncidenteController _controller;
        private readonly Mock<IIncidenteDAO> _serviceMockDAO;
        private readonly Mock<IIncidenteLogic> _serviceMockLogic;
        private readonly Mock<ILogger<IncidenteController>> _loggerMock;

        public IncidenteControllerTest()
        {
            _loggerMock = new Mock<ILogger<IncidenteController>>();
            _serviceMockDAO = new Mock<IIncidenteDAO>();
            _serviceMockLogic = new Mock<IIncidenteLogic>();
            _controller = new IncidenteController(
                _loggerMock.Object,
                _serviceMockDAO.Object,
                _serviceMockLogic.Object
            );
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Actualizar la lista de incidentes retorna true")]
        public Task UpdateIncidenteListReturnTrue()
        {
            _serviceMockLogic
                .Setup(x => x.UpdateIncidenteRegisters())
                .Returns(It.IsAny<int>());

            var result = _controller.UpdateListado();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir todos los Asegurados retorna false")]
        public Task UpdateIncidenteListReturnFalse()
        {
            _serviceMockLogic
                .Setup(x => x.UpdateIncidenteRegisters())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.UpdateListado();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "Controller: Cargar una lista de incidentes sin solicitudes retorna true")]
        public Task GetIncidentesWithoutSolicitudesReturnTrue()
        {
            _serviceMockDAO
                .Setup(x => x.GetAllWithoutSolicitud())
                .Returns(It.IsAny<ICollection<IncidenteToShowDTO>>());

            var result = _controller.GetAllWithoutSolicitud();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir el listado de Incidentes sin solicitud retorna false")]
        public Task GetIncidentesWithoutSolicitudesReturnFalse()
        {
            _serviceMockDAO
                .Setup(x => x.GetAllWithoutSolicitud())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetAllWithoutSolicitud();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Cargar  una lista de todos los incidentes retorna true")]
        public Task GetIncidentesReturnTrue()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Returns(It.IsAny<ICollection<IncidenteToShowDTO>>());

            var result = _controller.GetAll();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar cargar  una lista de todos los incidentes retorna false")]
        public Task GetIncidentesExceptionReturnFalse()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetAll();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Buscar la data de un incidente en detalle retorna true")]
        public async Task GetDetailedIncidenteReturnTrue()
        {
            
            Guid incidenteId = Guid.NewGuid();
            _serviceMockLogic
                .Setup(x => x.GetDetailedIncidente(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<IncidenteDTO>());

            var result = await _controller.GetDetaledIncidenteById(incidenteId);

            Assert.True(result.Success);
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al buscar la data de un incidente en detalle retorna false")]
        public async Task GetDetailedIncidenteExceptionReturnFalse()
        {
            Guid incidenteId = Guid.NewGuid();
            _serviceMockLogic
                .Setup(x => x.GetDetailedIncidente(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.GetDetaledIncidenteById(incidenteId);
            Assert.False(ex.Success);
        }
    }
}