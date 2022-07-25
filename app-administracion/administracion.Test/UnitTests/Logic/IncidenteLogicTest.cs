/*
using Microsoft.Extensions.Logging;
using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using administracion.DataAccess.DAOs.Logic;
using administracion.Conections.rabbit;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;
using  administracion.DataAccess.Enums;

namespace administracion.Test.UnitTests.Logic
{
    public class IncidenteLogicTest
    {
        private readonly IncidenteLogic _logic;
        private readonly Mock<IIncidenteDAO> _serviceMockIncidente;
        private readonly Mock<IProductorRabbit> _serviceMockRabbit;

        public IncidenteLogicTest()
        {
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
                .Returns(1);
            int result = _logic.RegisterIncidente(incidente);

            Assert.Equal(1,result);
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
                .Returns(1);
            
            int result = _logic.UpdateIncidenteState(It.IsAny<Guid>(),It.IsAny<EstadoIncidente>());
            Assert.Equal(1,result);
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
*/
