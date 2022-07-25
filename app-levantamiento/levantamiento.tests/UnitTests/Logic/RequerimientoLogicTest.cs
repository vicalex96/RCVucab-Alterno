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
    public class RequerimientoLogicTest
    {
        private readonly RequerimientoLogic _logic;
        private readonly Mock<ISolicitudReparacionDAO> _serviceMocksolicitudDAO;
        private readonly Mock<IParteDAO> _serviceMockparteDAO;
        private readonly Mock<IRequerimientoDAO> _serviceMockRequerimientoDAO;

        public RequerimientoLogicTest()
        {
            _serviceMockRequerimientoDAO = new Mock<IRequerimientoDAO>();
            _serviceMocksolicitudDAO = new Mock<ISolicitudReparacionDAO>();
            _serviceMockparteDAO = new Mock<IParteDAO>();
            _logic = new RequerimientoLogic(_serviceMockRequerimientoDAO.Object, _serviceMocksolicitudDAO.Object, _serviceMockparteDAO.Object);
        }

        
        

        public class RequerimientoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new RequerimientoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        parteId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        descripcion = "Puerta debe ser reparada",
                        tipoRequerimiento = "Reparacion",
                        cantidad = 1
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para poder registrar un requerimiento retorna true")]
        [ClassData(typeof(RequerimientoClassData))]
        public Task ShouldRegisterRequerimientoReturnTrue(RequerimientoRegisterDTO requerimiento)
        {
            _serviceMocksolicitudDAO.Setup(x => x.GetSolicitudById(It.IsAny<Guid>()))
                .Returns(new SolicitudesReparacionDTO());
            _serviceMockparteDAO.Setup(x => x.GetParteById(It.IsAny<Guid>()))
                .Returns(new ParteDTO());
            
            _serviceMockRequerimientoDAO.Setup(x => x.RegisterRequerimiento(It.IsAny<Requerimiento>()))
                .Returns(true);

            bool result = _logic.RegisterRequerimiento(requerimiento);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para poder registrar un requerimiento retorna RCVNullException al no existir la solicitud")]
        [ClassData(typeof(RequerimientoClassData))]
        public Task ShouldRegisterRequerimientoRetrunRCVNullExceptionSolicitud(RequerimientoRegisterDTO requerimiento)
        {
            _serviceMocksolicitudDAO.Setup(x => x.GetSolicitudById(It.IsAny<Guid>()));
            
            Assert.Throws<RCVNullException>(() => _logic.RegisterRequerimiento(requerimiento));
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para poder registrar un requerimiento retorna RCVNullException al no existir partes")]
        [ClassData(typeof(RequerimientoClassData))]
        public Task ShouldRegisterRequerimientoRetrunRCVNullExceptionParte(RequerimientoRegisterDTO requerimiento)
        {
            _serviceMocksolicitudDAO.Setup(x => x.GetSolicitudById(It.IsAny<Guid>()))
                .Returns(new SolicitudesReparacionDTO());
            _serviceMockparteDAO.Setup(x => x.GetParteById(It.IsAny<Guid>()));
            
            _serviceMockRequerimientoDAO.Setup(x => x.RegisterRequerimiento(It.IsAny<Requerimiento>()))
                .Returns(true);

            Assert.Throws<RCVNullException>(() => _logic.RegisterRequerimiento(requerimiento));
            return Task.CompletedTask;
        }

    }
}