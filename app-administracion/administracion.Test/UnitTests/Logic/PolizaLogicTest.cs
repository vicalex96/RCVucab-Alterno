using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.BussinesLogic.LogicClasses;
using administracion.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class PolizaLogicTest
    {
        private readonly PolizaLogic _logic;
        private readonly Mock<IPolizaDAO> _serviceMockPoliza;
        private readonly Mock<IVehiculoDAO> _serviceMockVehiculo;
        private readonly Mock<IAseguradoDAO> _serviceMockAsegurado;

        private readonly Mock<IAdminDBContext> _contextMock;
        public PolizaLogicTest()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _serviceMockPoliza = new Mock<IPolizaDAO>();
            _serviceMockVehiculo = new Mock<IVehiculoDAO>();
            _serviceMockAsegurado = new Mock<IAseguradoDAO>();
            _logic = new PolizaLogic(_serviceMockVehiculo.Object,_serviceMockAsegurado.Object, _serviceMockPoliza.Object);
        }

        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una poliza retorna True")]
        public Task ShouldRegisterPolizaReturnTrue()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO{
                    fechaRegistro = DateTime.ParseExact("01-01-2020","dd-mm-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("02-01-2020","dd-mm-yyyy",null),
                    tipoPoliza = "DaniosATerceros"
                };
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(It.IsAny<PolizaDTO>());
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO{
                    asegurado = new AseguradoDTO()
                });

            _serviceMockPoliza
                .Setup(x => x.RegisterPoliza(It.IsAny<Poliza>()))
                .Returns(true);
            
            bool result = _logic.RegisterPoliza(poliza);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una poliza retorna RCVNullException")]
        public Task ShouldRegisterPolizaReturnRCVNullException()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO();
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(new PolizaDTO());
            
            Assert.Throws<RCVNullException>(
                () => _logic.RegisterPoliza(poliza)
            );
            return Task.CompletedTask;
        }
        
        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una poliza, el vehiculo no tiene asegurado retorna RCVAsociationException")]
        public Task ShouldRegisterPolizaReturnRCVAsociationException()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO();
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(It.IsAny<PolizaDTO>());
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO());
            
            Assert.Throws<RCVAsociationException>(
                () => _logic.RegisterPoliza(poliza)
            );
            return Task.CompletedTask;
        }

        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una poliza,el orden de las fechas no es valido retorna RCVDateOrderException")]
        public Task ShouldRegisterPolizaReturnRCVDateOrderException()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO{
                    fechaRegistro = DateTime.ParseExact("10-01-2020","dd-mm-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("02-01-2020","dd-mm-yyyy",null),
                    tipoPoliza = "DaniosATerceros"
                };
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(It.IsAny<PolizaDTO>());
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO{
                    asegurado = new AseguradoDTO()
                });

            _serviceMockPoliza
                .Setup(x => x.RegisterPoliza(It.IsAny<Poliza>()))
                .Returns(true);
            
            Assert.Throws<RCVDateOrderException>(
                () => _logic.RegisterPoliza(poliza)
            );
            return Task.CompletedTask;
        }

        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una póliza,el tipo de póliza no es valido retorna RCVInvalidFieldException")]
        public Task ShouldRegisterPolizaReturnRCVInvalidFieldException()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO{
                    fechaRegistro = DateTime.ParseExact("01-01-2020","dd-mm-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("02-01-2020","dd-mm-yyyy",null),
                    tipoPoliza = "limitada"
                };
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(It.IsAny<PolizaDTO>());
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO{
                    asegurado = new AseguradoDTO()
                });
            
            Assert.Throws<RCVInvalidFieldException>(
                () => _logic.RegisterPoliza(poliza)
            );
            return Task.CompletedTask;
        }

        [Fact (DisplayName = "Logic: Ejecuta la lógica para registrar una póliza,ocurrió un error retorna RCVException")]
        public Task ShouldRegisterPolizaReturnRCVException()
        {
            PolizaRegisterDTO poliza = new PolizaRegisterDTO{
                    fechaRegistro = DateTime.ParseExact("01-01-2020","dd-mm-yyyy",null),
                    fechaVencimiento = DateTime.ParseExact("02-01-2020","dd-mm-yyyy",null),
                    tipoPoliza = "DaniosATerceros"
                };
            _serviceMockPoliza
                .Setup(x => x.GetPolizaByVehiculoGuid(It.IsAny<Guid>()))
                .Returns(It.IsAny<PolizaDTO>());
            _serviceMockVehiculo
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO{
                    asegurado = new AseguradoDTO()
                });
            _serviceMockPoliza
                .Setup(x => x.RegisterPoliza(It.IsAny<Poliza>()))
                .Throws( new Exception());
            
            Assert.Throws<RCVException>(
                () => _logic.RegisterPoliza(poliza)
            );
            return Task.CompletedTask;
        }
    }
}