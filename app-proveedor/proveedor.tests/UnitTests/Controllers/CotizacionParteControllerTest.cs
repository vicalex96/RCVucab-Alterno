using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using proveedor.BussinesLogic.DTOs;
using proveedor.Controllers;
using proveedor.Exceptions;
using proveedor.Persistence.DAOs;
using proveedor.Persistence.DAOs.Implementations;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CotizacionParte.Test.UnitTests.Controllers
{
    public class CotizacionParteControllerTest
    {
        private readonly CotizacionParteController _controller;
        private readonly Mock<ICotizacionParteDAO> _serviceMock;
        private readonly Mock<ILogger<CotizacionParteController>> _loggerMock;

        public CotizacionParteControllerTest()
        {
            _loggerMock = new Mock<ILogger<CotizacionParteController>>();
            _serviceMock = new Mock<ICotizacionParteDAO>();
            _controller = new CotizacionParteController(_loggerMock.Object, _serviceMock.Object);
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }
        //Registrar Cotizacion parte
        [Fact(DisplayName = "Controller: Registrar CotizacionParte")]
        public Task RegisterCotizacionParte()
        {
            _serviceMock
                .Setup(x => x.createCotizacionParte(It.IsAny<CotizacionParteDTO>()))
                .Returns(It.IsAny<string>());
              
            var result = _controller.createCotizacionParte(It.IsAny<CotizacionParteDTO>());

            Assert.IsType<ApplicationResponse<string>>(result);
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "Controller: Registrar Cotizacion Parte regresa una excepcion")]
        public Task RegisterIncidenteException()
        {
            _serviceMock
                .Setup(x => x.createCotizacionParte(It.IsAny<CotizacionParteDTO>()))
                .Throws(new ProveedorException("",new Exception()));

            var ex = _controller.createCotizacionParte(It.IsAny<CotizacionParteDTO>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
        //Consultar Cotizaciones de Partes por estado

        [Theory(DisplayName = "Controller: Obtener cotizaciones Partes por estado")]
        [InlineData(EstadoCotPt.Pendiente)]
        public Task GetCotizacionParteByestado(EstadoCotPt estado)
        {
            _serviceMock.Setup( x => x.GetCotizacionPartesByestado(It.IsAny<EstadoCotPt>()))
            .Returns(new List<CotizacionParteDTO>());
            var result = _controller.GetCotizacionPartesByestado(It.IsAny<EstadoCotPt>());
            
            Assert.IsType<ApplicationResponse<List<CotizacionParteDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Cotizaciones de partes por estado provoca una excepcion")]
        public Task GetActiveIncidentesException()
        {
            _serviceMock
                .Setup(x => x.GetCotizacionPartesByestado(It.IsAny<EstadoCotPt>()))
                .Throws(new ProveedorException("",new Exception()));

            var ex = _controller.GetCotizacionPartesByestado(It.IsAny<EstadoCotPt>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

         //Consultar Cotizaciones de Partes 



        [Fact(DisplayName = "Controller: Obtener cotizaciones Partes ")]
        public Task GetCotizacionPartes()
        {
            _serviceMock.Setup( x => x.GetCotizacionPartes())
            .Returns(new List<CotizacionParteDTO>());
            var result = _controller.GetCotizacionPartes();
            
            Assert.IsType<ApplicationResponse<List<CotizacionParteDTO>>>(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Cotizaciones de partes  provoca una excepcion")]
        public Task GetCotizacionPartesException()
        {
            _serviceMock
                .Setup(x => x.GetCotizacionPartes())
                .Throws(new ProveedorException("",new Exception()));

            var ex = _controller.GetCotizacionPartes();

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }

        //Cambiar estado de Cotizacion Parte
        [Fact(DisplayName = "Controller: Actualizar Estado CotPat")]
        public Task UpdateIncidenteState()
        {
            _serviceMock.Setup( x => x.actualizarCotizacionParte(It.IsAny<Guid>(),It.IsAny<EstadoCotPt>()))
            .Returns(It.IsAny<string>());
            var result = _controller.actualizarCotizacionParte(It.IsAny<Guid>(),It.IsAny<EstadoCotPt>());
            
            Assert.IsType<ApplicationResponse<string>>(result);
            return Task.CompletedTask;
        }
       
        [Fact(DisplayName = "Controller: Actualizar Estado CotPat arroja excepcion")]
        public Task CreateIncidenteException()
        {
            _serviceMock
                .Setup(x => x.actualizarCotizacionParte(It.IsAny<Guid>(),It.IsAny<EstadoCotPt>()))
                .Throws(new ProveedorException("",new Exception()));

            var ex = _controller.actualizarCotizacionParte(It.IsAny<Guid>(),It.IsAny<EstadoCotPt>());

            Assert.False(ex.Success);

            return Task.CompletedTask;
        }
    }
}
