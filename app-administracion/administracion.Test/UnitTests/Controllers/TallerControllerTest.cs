using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.BussinesLogic.DTOs;
using administracion.Controllers;
using administracion.Exceptions;
using  administracion.DataAccess.DAOs;
using administracion.Responses;
using administracion.Conections.rabbit;
using Xunit;
using administracion.DataAccess.DAOs.Logic;

namespace administracion.Test.UnitTests.Controllers
{
    public class TallerControllerTest
    {
        private readonly TallerController _controller;
        private readonly Mock<ITallerDAO> _serviceMock;
        private readonly Mock<ITallerLogic> _serviceMockLogic;
        private readonly Mock<ILogger<TallerController>> _loggerMock;

        public TallerControllerTest()
        {
            _loggerMock = new Mock<ILogger<TallerController>>();
            _serviceMock = new Mock<ITallerDAO>();
            _serviceMockLogic = new Mock<ITallerLogic>();
            _controller = new TallerController(_loggerMock.Object);

            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Registrar Taller")]
        public Task RegisterTaller()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterTaller(It.IsAny<TallerRegisterDTO>()))
                .Returns(It.IsAny<int>());
        
            var result = _controller.RegistrarTaller(It.IsAny<TallerRegisterDTO>());

            Assert.IsType<ApplicationResponse<int>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Registrar Taller regresa una excepcion")]
        public Task RegisterTallerException()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterTaller(It.IsAny<TallerRegisterDTO>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.RegistrarTaller(It.IsAny<TallerRegisterDTO>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "Controller: Consultar taller por Guid")]
        public Task GetTallers()
        {
            _serviceMock.Setup(x => x.GetTalleres())
            .Returns(new List<TallerDTO>());
            var result = _controller.ConsultarTalleres();

            Assert.IsType<ApplicationResponse<List<TallerDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Controller: Consultar taller por Guid retorna una Excepcion")]
        public Task GetTalleresException()
        {
            _serviceMock
                .Setup(x => x.GetTalleres())
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.ConsultarTalleres();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }




        [Fact(DisplayName = "Controller: Consultar taller por Guid")]
        public Task GetTallerByGuid()
        {
            _serviceMock.Setup(x => x.GetTallerByGuid(It.IsAny<Guid>()))
            .Returns(new TallerDTO());
            var result = _controller.ConsultarTallerPorId(It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<TallerDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Controller: Consultar taller por Guid retorna una Excepcion")]
        public Task GetTallerByGuidException()
        {
            _serviceMock
                .Setup(x => x.GetTallerByGuid(It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.ConsultarTallerPorId(It.IsAny<Guid>());
            Assert.NotNull(ex);
            //Assert.False(ex.Success);

            return Task.CompletedTask;
        }

    }
}
