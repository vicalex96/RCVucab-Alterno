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
    public class SolicitudControllerTest
    {
        private readonly SolicitudController _controller;
        private readonly Mock<ISolicitudReparacionDAO> _serviceMockDAO;
        private readonly Mock<ISolicitudReparacionLogic> _serviceMockLogic;
        private readonly Mock<ILogger<SolicitudController>> _loggerMock;

        public SolicitudControllerTest()
        {
            _loggerMock = new Mock<ILogger<SolicitudController>>();
            _serviceMockDAO = new Mock<ISolicitudReparacionDAO>();
            _serviceMockLogic = new Mock<ISolicitudReparacionLogic>();
            _controller = new SolicitudController(
                _loggerMock.Object,
                _serviceMockDAO.Object,
                _serviceMockLogic.Object
            );
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.ActionDescriptor = new ControllerActionDescriptor();
        }

        [Fact(DisplayName = "Controller: intenta obtener un listado completo de solicitudes retorna true")]
        public Task GetAllSolicitudesRetrunTrue()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Returns(It.IsAny<List<SolicitudesReparacionDTO>>());

            var result = _controller.GetAll();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al obtener un listado completo de solicitudes retorna false")]
        public Task GetAllSolicitudesRetrunFalse()
        {
            _serviceMockDAO
                .Setup(x => x.GetAll())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetAll();
            Assert.False(ex.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: intenta obtener un listado completo de solicitudes que no tienen taller retorna true")]
        public Task GetAllSolicitudesWithoutTallerRetrunTrue()
        {
            _serviceMockDAO
                .Setup(x => x.GetSolicitudWithoutTaller())
                .Returns(It.IsAny<List<SolicitudesReparacionDTO>>());

            var result = _controller.GetSolicitudesSinTaller();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar obtener un listado completo de solicitudes que no tienen taller  retorna false")]
        public Task GetAllSolicitudesWithoutTallerRetrunFalse()
        {
            _serviceMockDAO
                .Setup(x => x.GetSolicitudWithoutTaller())
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetSolicitudesSinTaller();
            Assert.False(ex.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: intenta obtener una solicitud según su Id retorna true")]
        public Task GetSolicitudByIdRetrunTrue()
        {
            Guid solicitudId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetSolicitudById(It.IsAny<Guid>()))
                .Returns(It.IsAny<SolicitudesReparacionDTO>());

            var result = _controller.GetSolicitud(solicitudId);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar obtener una solicitud según su Id  retorna false")]
        public Task GetSolicitudByIdRetrunFalse()
        {
            Guid solicitudId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetSolicitudById(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetSolicitud(solicitudId);
            Assert.False(ex.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: intenta obtener una solicitud según su el Id del incidente retorna true")]
        public Task GetSolicitudByIncidenteIdRetrunTrue()
        {
            Guid incidenteId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetSolicitudByIncidenteId(It.IsAny<Guid>()))
                .Returns(It.IsAny<List<SolicitudesReparacionDTO>>());

            var result = _controller.GetSolicitudesByIncidente(incidenteId);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar obtener una solicitud según el Id del incidente retorna false")]
        public Task GetSolicitudByIncidenteIdRetrunFalse()
        {
            Guid incidenteId = Guid.NewGuid();
            _serviceMockDAO
                .Setup(x => x.GetSolicitudByIncidenteId(It.IsAny<Guid>()))
                .Throws(new RCVException("",new Exception()));

            var ex = _controller.GetSolicitudesByIncidente(incidenteId);
            Assert.False(ex.Success);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "Controller: registra una solicitud de reparacion retorna true")]
        public async  Task RegisterSolicitudReparacionRetrunTrue()
        {
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterSolicitud(
                    It.IsAny<SolicitudRepacionRegisterDTO>() )
                )
                .ReturnsAsync(It.IsAny<bool>());

            var result = await _controller.RegistrarSolicitud(solicitud);

            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar registrar una solicitud de reparacion retorna false")]
        public async Task RegisterSolicitudReparacionRetrunFalse()
        {
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterSolicitud(
                    It.IsAny<SolicitudRepacionRegisterDTO>() )
                )
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.RegistrarSolicitud(solicitud);
            Assert.False(ex.Success);
        }

        [Fact(DisplayName = "Controller: carga la cola con los registros realizados retorna true")]
        public Task TryChageQueueRetrunTrue()
        {
            var result = _controller.RefrescarCola();

            Assert.True(result.Success);
            return Task.CompletedTask;
        }

        /*[Fact(DisplayName = "Controller: Obtener Excepticion al intentar registrar una solicitud de reparacion retorna false")]
        public async Task RegisterSolicitudReparacionRetrunFalse()
        {
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterSolicitud(
                    It.IsAny<SolicitudRepacionRegisterDTO>() )
                )
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.RegistrarSolicitud(solicitud);
            Assert.False(ex.Success);
        }*/

        [Fact(DisplayName = "Controller: intenta encontra el taller adecuado para la solicitud retorna true")]
        public Task AsociateTallerWithSolicitudRetrunTrue()
        {
            Guid solicitudId = Guid.NewGuid();

            var result = _controller.AsociarTaller(solicitudId);

            Assert.True(result.Success);
            return Task.CompletedTask;
        }
        
        /*
        [Fact(DisplayName = "Controller: Obtener Excepticion al intentar registrar una solicitud de reparacion retorna false")]
        public async Task AsociateTallerWithSolicitudRetrunFalse()
        {
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockLogic
                .Setup(x => x.RegisterSolicitud(
                    It.IsAny<SolicitudRepacionRegisterDTO>() )
                )
                .Throws(new RCVException("",new Exception()));

            var ex = await _controller.RegistrarSolicitud(solicitud);
            Assert.False(ex.Success);
        }
        */
    }
}