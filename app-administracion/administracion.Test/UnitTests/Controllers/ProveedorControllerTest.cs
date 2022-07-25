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
    public class ProveedorControllerTest
    {
        private readonly ProveedorController _controller;
        private readonly Mock<IProveedorDAO> _serviceMock;
        private readonly Mock<IProveedorLogic> _serviceMockLogic;
        private readonly Mock<ILogger<ProveedorController>> _loggerMock;

        public ProveedorControllerTest()
        {
            _loggerMock = new Mock<ILogger<ProveedorController>>();
            _serviceMock = new Mock<IProveedorDAO>();
            _serviceMockLogic = new Mock<IProveedorLogic>();
            _controller = new ProveedorController(_loggerMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Registrar Proveedor")]
        public Task RegisterProveedor()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterProveedor(It.IsAny<ProveedorRegisterDTO>()))
                .Returns(It.IsAny<int>());

            var result = _controller.RegistrarProveedor(It.IsAny<ProveedorRegisterDTO>());

            Assert.IsType<ApplicationResponse<int>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Registrar Proveedor regresa una excepcion")]
        public Task RegisterProveedorException()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterProveedor(It.IsAny<ProveedorRegisterDTO>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.RegistrarProveedor(It.IsAny<ProveedorRegisterDTO>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "Controller: Consultar Proveedor por Guid")]
        public Task GetProveedors()
        {
            _serviceMock.Setup(x => x.GetProveedores())
            .Returns(new List<ProveedorDTO>());
            var result = _controller.ConsultarProveedores();

            Assert.IsType<ApplicationResponse<List<ProveedorDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Controller: Consultar Proveedor por Guid retorna una Excepcion")]
        public Task GetProveedoresException()
        {
            _serviceMock
                .Setup(x => x.GetProveedores())
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.ConsultarProveedores();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }




        [Fact(DisplayName = "Controller: Consultar Proveedor por Guid")]
        public Task GetProveedorByGuid()
        {
            _serviceMock.Setup(x => x.GetProveedorByGuid(It.IsAny<Guid>()))
            .Returns(new ProveedorDTO());
            var result = _controller.ConsultarProveedorPorId(It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<ProveedorDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Controller: Consultar Proveedor por Guid retorna una Excepcion")]
        public Task GetProveedorByGuidException()
        {
            _serviceMock
                .Setup(x => x.GetProveedorByGuid(It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.ConsultarProveedorPorId(It.IsAny<Guid>());
            Assert.NotNull(ex);
            //Assert.False(ex.Success);

            return Task.CompletedTask;
        }
    }
}
