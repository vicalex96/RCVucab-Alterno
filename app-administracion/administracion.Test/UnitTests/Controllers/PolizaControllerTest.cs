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
using administracion.BussinesLogic.LogicClasses;

namespace RCVUcab.Test.UnitTests.Controllers
{
    public class PolizaControllerTest
    {
        private readonly PolizaController _controller;
        private readonly Mock<IPolizaDAO> _serviceMock;
        private readonly Mock<IPolizaLogic> _serviceMockPolizaLogic;
        private readonly Mock<ILogger<PolizaController>> _loggerMock;

        public PolizaControllerTest()
        {
            _loggerMock = new Mock<ILogger<PolizaController>>();
            _serviceMock = new Mock<IPolizaDAO>();
            _serviceMockPolizaLogic = new Mock<IPolizaLogic>();
            _controller = new PolizaController(_loggerMock.Object, _serviceMock.Object, _serviceMockPolizaLogic.Object);
            
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: Registrar polizas")]
        public Task RegistrerPoliza()
        {
            _serviceMockPolizaLogic
                .Setup(x => x.RegisterPoliza(It.IsAny<PolizaRegisterDTO>()))
                .Returns(It.IsAny<bool>);

            var result = _controller.registrarPoliza(It.IsAny<PolizaRegisterDTO>());

            Assert.IsType<ApplicationResponse<bool>>(result);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener Excepticion al Registrar polizas")]
        public Task RegistrerPolizaException()
        {
            _serviceMockPolizaLogic
                .Setup(x => x.RegisterPoliza(It.IsAny<PolizaRegisterDTO>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.registrarPoliza(It.IsAny<PolizaRegisterDTO>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Obtener una Poliza a traves de su vehiculo guid")]
        public Task GetPolizaByVehiculoID()
        {
            _serviceMock.Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
            .Returns(new PolizaDTO());
            var result = _controller.consultarPolizaDeVehiculo(It.IsAny<Guid>());

            Assert.IsType<ApplicationResponse<PolizaDTO>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion buscar una poliza por su vehiculo")]
        public Task GetPolizaByVehiculoIDException()
        {
            _serviceMock
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Throws(new RCVException("", new Exception()));

            var ex = _controller.consultarPolizaDeVehiculo(It.IsAny<Guid>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

    }
}
