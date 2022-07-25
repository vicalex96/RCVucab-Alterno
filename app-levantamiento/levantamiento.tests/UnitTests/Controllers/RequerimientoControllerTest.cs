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
    public class RequerimientoControllerTest
    {
        private readonly RequerimientoController _controller;
        private readonly Mock<IRequerimientoDAO> _serviceMockDAO;
        private readonly Mock<IRequerimientoLogic> _serviceMockLogic;
        private readonly Mock<ILogger<RequerimientoController>> _loggerMock;

        public RequerimientoControllerTest()
        {
            _loggerMock = new Mock<ILogger<RequerimientoController>>();
            _serviceMockDAO = new Mock<IRequerimientoDAO>();
            _serviceMockLogic = new Mock<IRequerimientoLogic>();
            _controller = new RequerimientoController(
                _loggerMock.Object,
                _serviceMockDAO.Object,
                _serviceMockLogic.Object
            );
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Muestra la informacion de una solicitud retrona true")]
        public Task ShowAllPartesReturnTrue()
        {
            Guid RequerimientoId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetRequerimientosBySolicitudId(It.IsAny<Guid>()))
                .Returns(It.IsAny<List<RequerimientoDTO>>());

            var result = _controller.GetRequerimientosBySolicitudId(RequerimientoId);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar mostrar el listado completo de partes retrona false")]
        public Task ShowAllPartesReturnFalse()
        {
            Guid RequerimientoId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetRequerimientosBySolicitudId(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetRequerimientosBySolicitudId(RequerimientoId);
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "Controller: intenta registrar un requerimiento retrona true")]
        public Task RegisterRequerimientoReturnTrue()
        {
            RequerimientoRegisterDTO requerimiento = new RequerimientoRegisterDTO();

            _serviceMockLogic
                .Setup(x => x.RegisterRequerimiento(It.IsAny<RequerimientoRegisterDTO>()))
                .Returns(It.IsAny<bool>());

            var result = _controller.RegisterRequerimiento(requerimiento);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "Controller: intenta registrar un requerimiento retrona false")]
        public Task RegisterRequerimientoReturnFalse()
        {
            RequerimientoRegisterDTO requerimiento = new RequerimientoRegisterDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterRequerimiento(It.IsAny<RequerimientoRegisterDTO>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.RegisterRequerimiento(requerimiento);
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
    }
}