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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RCVUcab.Test.UnitTests.Controllers
{
    public class VehiculoControllerTest
    {
        private readonly VehiculoController _controller;
        private readonly Mock<IVehiculoDAO> _serviceMock;
        private readonly Mock<ILogger<VehiculoController>> _loggerMock;

        public VehiculoControllerTest()
        {
            _loggerMock = new Mock<ILogger<VehiculoController>>();
            _serviceMock = new Mock<IVehiculoDAO>();
            _controller = new VehiculoController(_loggerMock.Object, _serviceMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Obtener todos los Vehiculos")]
        public Task GetVehiculos()
        {
            _serviceMock
                .Setup(x => x.GetAllVehiculos())
                .Returns(new List<VehiculoDTO>());

            var result = _controller.GetAllVehiculos();

            Assert.IsType<ApplicationResponse<List<VehiculoDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir todos los Vehiculos")]
        public Task GetVehiculosException()
        {
            _serviceMock
                .Setup(x => x.GetAllVehiculos())
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.GetAllVehiculos();
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener un Vehiculo a traves de su guid")]
        public Task GetVehiculoByGuid()
        {
            _serviceMock.Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
            .Returns(new VehiculoDTO());
            var result = _controller.GetVehiculoByGuid(It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<VehiculoDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al pedir Vehiculo por Guid")]
        public Task GetVehiculoByGuidException()
        {
            _serviceMock
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.GetVehiculoByGuid(It.IsAny<Guid>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: registrar un vehiculo")]
        public Task RegisterVehiculo()
        {
            _serviceMock.Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoSimpleDTO>()))
            .Returns(It.IsAny<bool>());
            var result = _controller.createVehiculo(It.IsAny<VehiculoSimpleDTO>());

            Assert.IsType<ApplicationResponse<bool>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al registrar vehiculo")]
        public Task RegisterVehiculoException()
        {
            _serviceMock
                .Setup(x => x.RegisterVehiculo(It.IsAny<VehiculoSimpleDTO>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.createVehiculo(It.IsAny<VehiculoSimpleDTO>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: asociar el vehiculo con un asegurado")]
        public Task AssociateVehiculoWithAsegurado()
        {
            _serviceMock.Setup(x => x.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(It.IsAny<bool>());
            var result = _controller.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<bool>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Agregar asegurado al Vehiculo arroja excepcion")]
        public Task AssociateVehiculoWithAseguradoException()
        {
            _serviceMock
                .Setup(x => x.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.AddAsegurado(It.IsAny<Guid>(), It.IsAny<Guid>());
            Assert.False(ex.Success);

            return Task.CompletedTask;
        }


    }
}
