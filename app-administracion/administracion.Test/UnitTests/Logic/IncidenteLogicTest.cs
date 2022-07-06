using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.BussinesLogic.LogicClasses;
using administracion.Conections.rabbit;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class IncidenteLogicTest
    {
        private readonly IncidenteLogic _logic;
        private readonly Mock<IIncidenteDAO> _serviceMockIncidente;
        private readonly Mock<IProductorRabbit> _serviceMockRabbit;

        private readonly Mock<IAdminDBContext> _contextMock;
        public IncidenteLogicTest()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _serviceMockIncidente = new Mock<IIncidenteDAO>();
            _serviceMockRabbit = new Mock<IProductorRabbit>();
            _logic = new IncidenteLogic(_serviceMockIncidente.Object, _serviceMockRabbit.Object);
        }

        [Fact (DisplayName ="Logic: Registra Incidente y retorna True")]
        public Task ShouldExcuteRegisterIncidenteLogicReturnTrue()
        {   
            IncidenteRegisterDTO incidente = new IncidenteRegisterDTO();
            _serviceMockIncidente
                .Setup(x => x.RegisterIncidente(It.IsAny<Incidente>()))
                .Returns(true);
            var result = _logic.RegisterIncidente(incidente);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta registrar un incidente y retorna una ECVException")]
        public Task ShouldExcuteRegisterIncidenteLogicReturnRCVException()
        {
            IncidenteRegisterDTO incidente = new IncidenteRegisterDTO();
            _serviceMockIncidente
                .Setup(x => x.RegisterIncidente(It.IsAny<Incidente>()))
                .Throws(new Exception());

            Assert.Throws<RCVException>(() => _logic.RegisterIncidente(incidente));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Actualiza el estado de un incidente y retorna True")]
        public Task ShouldExcuteUpdateIncidenteLogicReturnTrue()
        {
            _serviceMockIncidente
                .Setup(x => x.GetIncidenteById(It.IsAny<Guid>()))
                .Returns(new IncidenteDTO());

            _serviceMockIncidente
                .Setup(x => x.UpdateIncidente(It.IsAny<Incidente>()))
                .Returns(true);
            
            var result = _logic.UpdateIncidenteState(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>());
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: intenta actualiza el estado de un incidente y al no encontrar incidente, retorna RCVNullException ")]
        public Task ShouldExcuteUpdateIncidenteLogicReturnRCVException()
        {
            _serviceMockIncidente
                .Setup(x => x.GetIncidenteById(It.IsAny<Guid>()))
                .Returns(It.IsAny<IncidenteDTO>());
            
            Assert.Throws<RCVNullException>(
                () => _logic.UpdateIncidenteState(
                    It.IsAny<Guid>(),
                    It.IsAny<EstadoIncidente>()
                    )
                );
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: intenta actualiza el estado de un incidente y no logra actualizar, retorna RCVUpdateException ")]
        public Task ShouldExcuteUpdateIncidenteLogicReturnRCVUpdateException()
        {
            _serviceMockIncidente
                .Setup(x => x.GetIncidenteById(It.IsAny<Guid>()))
                .Returns(new IncidenteDTO());

            _serviceMockIncidente
                .Setup(x => x.UpdateIncidente(It.IsAny<Incidente>()))
                .Throws(new RCVUpdateException(""));
        
            Assert.Throws<RCVUpdateException>(
                () => _logic.UpdateIncidenteState(
                    It.IsAny<Guid>(),
                    It.IsAny<EstadoIncidente>()
                    )
                );
            return Task.CompletedTask;
        }

    }
}
