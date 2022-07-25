using Moq;
using levantamiento.DataAccess.DAOs;
using levantamiento.DataAccess.Database;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.Logic;
using levantamiento.Conections.rabbit;
using levantamiento.Conections.APIs;
using levantamiento.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class IncidenteLogicTest
    {
        private readonly IncidenteLogic _logic;
        private readonly Mock<IConsumerRabbit> _serviceMockConsumer;
        private readonly Mock<IIncidenteAPI> _serviceMockAPI;
        private readonly Mock<ISolicitudReparacionDAO> _serviceMockSolicitudDAO;
        private readonly Mock<IIncidenteDAO> _serviceMockIncidenteDAO;


        public IncidenteLogicTest()
        {
            _serviceMockConsumer = new Mock<IConsumerRabbit>();
            _serviceMockIncidenteDAO = new Mock<IIncidenteDAO>();
            _serviceMockAPI = new Mock<IIncidenteAPI>();
            _serviceMockSolicitudDAO = new Mock<ISolicitudReparacionDAO>();
            _logic = new IncidenteLogic(_serviceMockConsumer.Object,  _serviceMockIncidenteDAO.Object, _serviceMockAPI.Object, _serviceMockSolicitudDAO.Object);
        }

        [Fact (DisplayName ="Logic: actualiza el listado de incidentes retorna True")]
        public Task ShouldExcuteRegisterIncidenteReturnTrue()
        {   
            _serviceMockConsumer.Setup(x => x.Consume()).Returns(new List<string>() { "register:00000000-0000-0000-0000-000000000000:00000000-0000-0000-0000-000000000000", "2:00000000-0000-0000-0000-000000000000:00000000-0000-0000-0000-000000000000", "3:00000000-0000-0000-0000-000000000000:00000000-0000-0000-0000-000000000000" });
            _serviceMockIncidenteDAO.Setup(x => x.RegisterIncidente(It.IsAny<Incidente>())).Returns(true);
            int result = _logic.UpdateIncidenteRegisters();
            Assert.Equal(3,result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta actaulizar el listado de incidentes retorna Excepcion")]
        public Task ShouldExcuteRegisterIncidenteReturnExeption()
        {   
            _serviceMockConsumer.Setup(x => x.Consume())
            .Throws(new Exception("Error"));
            Assert.Throws<RCVException>(() => _logic.UpdateIncidenteRegisters());
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Busca obtener los datos de un incidente en detalle")]
        public async Task ShouldGetIncidentDetailsReturnTrue()
        {   
            Guid incidenteId = Guid.NewGuid();
            
            _serviceMockAPI.Setup(x => x.GetIncidenteFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync( new IncidenteDTO());
            _serviceMockSolicitudDAO.Setup(x => x.GetSolicitudByIncidenteId(It.IsAny<Guid>()))
                .Returns( It.IsAny<List<SolicitudesReparacionDTO>>() );

            IncidenteDTO result = await _logic.GetDetailedIncidente(incidenteId);

            Assert.NotNull(result);
        }
    }
}