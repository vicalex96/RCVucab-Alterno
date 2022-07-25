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
    public class SolicitudLogicTest
    {
        private readonly SolicitudReparacionLogic _logic;
        private readonly Mock<ISolicitudReparacionDAO> _serviceMockSolicitudDAO;
        private readonly Mock<IIncidenteAPI> _serviceMockIncidenteAPI;
        private readonly Mock<IVehiculoAPI> _serviceMockVehiculoAPI;

        public SolicitudLogicTest()
        {
            _serviceMockSolicitudDAO = new Mock<ISolicitudReparacionDAO>();
            _serviceMockIncidenteAPI = new Mock<IIncidenteAPI>();
            _serviceMockVehiculoAPI = new Mock<IVehiculoAPI>();
            _logic = new SolicitudReparacionLogic(_serviceMockSolicitudDAO.Object, _serviceMockIncidenteAPI.Object, _serviceMockVehiculoAPI.Object);
        }

        
        public class ParteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new ParteDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "Puerta",
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        [Fact (DisplayName ="Logic: Ejecuta la logica de registro de solicitud retorna true")]
        public async Task ShouldExcuteRegisterIncidenteReturnTrue()
        {   
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockIncidenteAPI.Setup(x => x.GetIncidenteFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(new IncidenteDTO());
            _serviceMockVehiculoAPI.Setup(x => x.GetVehiculoFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(new VehiculoDTO());

            _serviceMockSolicitudDAO.Setup(x => x.RegisterSolicitud(It.IsAny<SolicitudReparacion>()))
                .Returns(true);
            bool result = await  _logic.RegisterSolicitud(solicitud);
            Assert.True(result);
        }

        [Fact (DisplayName ="Logic: Ejecuta la logica de registro de solicitud retorna RCVNullException al no haber Incidente")]
        public async Task ShouldExcuteRegisterIncidenteReturnRCVNullExceptionIncidente()
        {   
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockIncidenteAPI.Setup(x => x.GetIncidenteFromAdmin(It.IsAny<Guid>()));
            await Assert.ThrowsAsync<RCVNullException>( () =>  _logic.RegisterSolicitud(solicitud));
        }

        [Fact (DisplayName ="Logic: Ejecuta la logica de registro de solicitud retorna RCVNullException al no haber vehiculo")]
        public async Task ShouldExcuteRegisterIncidenteReturnRCVNullExceptionVehiculo()
        {   
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockIncidenteAPI.Setup(x => x.GetIncidenteFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(new IncidenteDTO());
            _serviceMockVehiculoAPI.Setup(x => x.GetVehiculoFromAdmin(It.IsAny<Guid>()));

            await Assert.ThrowsAsync<RCVNullException>( () =>  _logic.RegisterSolicitud(solicitud));
        }

        [Fact (DisplayName ="Logic: Ejecuta la logica de registro de solicitud retorna RCVException haber un error al registrar")]
        public async Task ShouldExcuteRegisterIncidenteReturnRCVException()
        {   
            SolicitudRepacionRegisterDTO solicitud = new SolicitudRepacionRegisterDTO();
            _serviceMockIncidenteAPI.Setup(x => x.GetIncidenteFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(new IncidenteDTO());
            _serviceMockVehiculoAPI.Setup(x => x.GetVehiculoFromAdmin(It.IsAny<Guid>()))
                .ReturnsAsync(new VehiculoDTO());

            _serviceMockSolicitudDAO.Setup(x => x.RegisterSolicitud(It.IsAny<SolicitudReparacion>()))
                .Throws(new Exception());

            await Assert.ThrowsAsync<RCVException>( () =>  _logic.RegisterSolicitud(solicitud));
        }
    }
}