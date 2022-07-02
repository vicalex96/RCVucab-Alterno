using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using proveedor.BussinesLogic.DTOs;
using proveedor.Controllers.Provider;
using proveedor.Exceptions;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace proveedor.Test.UnitTests.Controllers
{
    public class ProviderControllerTest
    {
        private readonly ProviderController _controller;
        private readonly Mock<IProviderDAO> _serviceMock;
        private readonly Mock<ILogger<ProviderController>> _loggerMock;

       /* public ProviderControllerTest()
        {
            _loggerMock = new Mock<ILogger<ProviderController>>();
            _serviceMock = new Mock<IProviderDAO>();
            _controller = new ProviderController(_loggerMock.Object, _serviceMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }*/

       /* [Fact(DisplayName = "Get Providers By Brand")]
        public Task GetProvidersByBrand()
        {
            _serviceMock
                .Setup(x => x.GetProvidersByBrand(It.IsAny<string>()))
                .Returns(new List<MarcaDTO>());
              
            var result = _controller.GetProvidersByBrand("");

            Assert.IsType<ApplicationResponse<List<MarcaDTO>>>(result);
            return Task.CompletedTask;
        }*/

       /* [Fact(DisplayName = "Get Providers By Brand with Exception")]
        public Task GetProvidersByBrandException()
        {
            _serviceMock
                .Setup(x => x.GetProvidersByBrand(It.IsAny<string>()))
                .Throws(new ProveedorException("", new Exception()));

            var ex = _controller.GetProvidersByBrand("");

            Assert.NotNull(ex);
            Assert.False(ex.Success);
            return Task.CompletedTask;
        }*/
    }
}
