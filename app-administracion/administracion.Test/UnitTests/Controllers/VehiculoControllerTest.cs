using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs.Logic;
using administracion.Controllers;
using administracion.Exceptions;
using  administracion.DataAccess.DAOs;
using administracion.Responses;
using Xunit;

namespace administracion.Test.UnitTests.Controllers
{
    public class VehiculoControllerTest
    {
        private readonly VehiculoController _controller;
        private readonly Mock<IVehiculoDAO> _serviceMockVehiculo;
        private readonly Mock<IVehiculoLogic> _serviceMockLogic;
        
        private readonly Mock<ILogger<VehiculoController>> _loggerMock;

        public VehiculoControllerTest()
        {
            _loggerMock = new Mock<ILogger<VehiculoController>>();
            _serviceMockVehiculo = new Mock<IVehiculoDAO>();
            _serviceMockLogic = new Mock<IVehiculoLogic>();
            
            _controller = new VehiculoController(_loggerMock.Object);

            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Obtener todos los Vehiculos retorna vehiculos")]
        public Task ShouldGetVehiculosReturnVehiculos()
        {
            _serviceMockVehiculo
                .Setup(x => x.GetAllVehiculos())
                .Returns(new List<VehiculoDTO>());

            var result = _controller.GetAllVehiculos();

            Assert.IsType<ApplicationResponse<List<VehiculoDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir todos los Vehiculos")]
        public Task ShouldGetVehiculosReturnException()
        {
            _serviceMockVehiculo
                .Setup(x => x.GetAllVehiculos())
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.GetAllVehiculos();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener un Vehiculo a traves de su guid")]
        public Task ShouldGetVehiculoByGuidReturnVehiculo()
        {
            _serviceMockVehiculo.Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
            .Returns(new VehiculoDTO());
            var result = _controller.GetVehiculoByGuid(It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<VehiculoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir Vehiculo por Guid")]
        public Task ShouldGetVehiculoByGuidReturnException()
        {
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.GetVehiculoByGuid(It.IsAny<Guid>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: registrar un vehiculo")]
        public Task ShouldRegisterVehiculoReturnTrue()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoRegisterDTO>()))
                .Returns(It.IsAny<int>());
            var result = _controller
                .createVehiculo(It.IsAny<VehiculoRegisterDTO>());

            Assert.IsType<ApplicationResponse<int>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al registrar vehiculo")]
        public Task ShouldRegisterVehiculoReturnException()
        {
            _serviceMockLogic
                .Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoRegisterDTO>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.createVehiculo(It.IsAny<VehiculoRegisterDTO>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: asociar el vehiculo con un asegurado")]
        public Task ShouldAssociateVehiculoWithAseguradoReturnTrue()
        {
            _serviceMockLogic
                .Setup(x => x.AddAseguradoToVehiculo(
                    It.IsAny<Guid>(), 
                    It.IsAny<Guid>())
                )
                .Returns(It.IsAny<int>());

            var result = _controller.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<int>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Agregar asegurado al Vehiculo arroja excepcion")]
        public Task ShouldAssociateVehiculoWithAseguradoReturnException()
        {
            _serviceMockLogic
                .Setup(x => x.AddAseguradoToVehiculo(
                    It.IsAny<Guid>(), 
                    It.IsAny<Guid>())
                )
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }


    }
}
