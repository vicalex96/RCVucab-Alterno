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
    public class ParteControllerTest
    {
        private readonly ParteController _controller;
        private readonly Mock<IParteDAO> _serviceMockDAO;
        private readonly Mock<IParteLogic> _serviceMockLogic;
        private readonly Mock<ILogger<ParteController>> _loggerMock;

        public ParteControllerTest()
        {
            _loggerMock = new Mock<ILogger<ParteController>>();
            _serviceMockDAO = new Mock<IParteDAO>();
            _serviceMockLogic = new Mock<IParteLogic>();
            _controller = new ParteController(
                _loggerMock.Object,
                _serviceMockDAO.Object,
                _serviceMockLogic.Object
            );
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Muestra el listado completo de partes retorna true")]
        public Task ShowAllPartesReturnTrue()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Returns(It.IsAny<List<ParteDTO>>());

            var result = _controller.GetAll();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar mostrar el listado completo de partes retorna false")]
        public Task ShowAllPartesReturnFalse()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetAll();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        

        [Fact(DisplayName = "Controller: realiza un registro de la parte ingresada retorna true")]
        public Task RegisterParteReturnTrue()
        {
            ParteDTO parte = new ParteDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterParte(It.IsAny<ParteDTO>()))
                .Returns(It.IsAny<bool>());

            var result = _controller.RegisterParte(parte);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar mostrar el listado completo de partes retorna false")]
        public Task RegisterParteReturnFalse()
        {
            ParteDTO parte = new ParteDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterParte(It.IsAny<ParteDTO>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.RegisterParte(parte);
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
    }
}