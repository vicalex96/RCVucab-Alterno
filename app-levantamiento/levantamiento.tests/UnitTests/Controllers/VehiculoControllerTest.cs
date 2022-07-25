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
using levantamiento.Conections.APIs;

namespace administracion.Test.UnitTests.Controllers
{
    public class VehiculoControllerTest
    {
        private readonly VehiculoController _controller;
        private readonly Mock<IVehiculoAPI> _serviceMockAPI;
        private readonly Mock<ILogger<VehiculoController>> _loggerMock;

        public VehiculoControllerTest()
        {
            _loggerMock = new Mock<ILogger<VehiculoController>>();
            _serviceMockAPI = new Mock<IVehiculoAPI>();
            _controller = new VehiculoController(
                _loggerMock.Object,
                _serviceMockAPI.Object
            );
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: intenta buscar un vehiculo por su id retorna true")]
        public async Task GetVehiculoByIDReturnTrue()
        {
            Guid vehiculoId = Guid.NewGuid();
            _serviceMockAPI
                .Setup(x => x.GetVehiculoFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(It.IsAny<VehiculoDTO>());

            var result = await _controller.GetVehiculoById(vehiculoId);

            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al intenta buscar un vehiculo por su id retorna false")]
        public async  Task GetVehiculoByIDReturnFalse()
        {
            Guid vehiculoId = Guid.NewGuid();
            _serviceMockAPI
                .Setup(x => x.GetVehiculoFromAdmin(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.GetVehiculoById(vehiculoId);
            Assert.False(ex.Success);
        }
        

        [Fact(DisplayName = "Controller: realiza un registro de vehiculo de 3ro retorna true")]
        public async  Task RegisterVehiculoReturnTrue()
        {
            VehiculoRegisterDTO vehiculo = new VehiculoRegisterDTO();
            _serviceMockAPI
                .Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoRegisterDTO>()))
                .ReturnsAsync(It.IsAny<bool>());

            var result = await _controller.RegisterVehiculo(vehiculo);

            Assert.True(result.Success);
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al realizar un registro de vehiculo de 3ro  retorna false")]
        public async Task RegisterVehiculoReturnFalse()
        {
            VehiculoRegisterDTO vehiculo = new VehiculoRegisterDTO();
            _serviceMockAPI
                .Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoRegisterDTO>()))
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.RegisterVehiculo(vehiculo);

            Assert.False(ex.Success);
        }
    }
}