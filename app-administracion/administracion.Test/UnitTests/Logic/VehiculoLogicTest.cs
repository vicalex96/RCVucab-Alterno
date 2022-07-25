/*
using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using administracion.DataAccess.DAOs.Logic;
using administracion.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class VehiculoLogicTest
    {
        private readonly VehiculoLogic _logic;
        private readonly Mock<IVehiculoDAO> _serviceMockVehiculo;
        private readonly Mock<IAseguradoDAO> _serviceMockAsegurado;

        public VehiculoLogicTest()
        {
            _serviceMockVehiculo = new Mock<IVehiculoDAO>();
            _serviceMockAsegurado = new Mock<IAseguradoDAO>();
            _logic = new VehiculoLogic(_serviceMockVehiculo.Object, _serviceMockAsegurado.Object);
        }

        public class VehiculoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new VehiculoRegisterDTO()
                    {
                        anioModelo = 2003,
                        fechaCompra = new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local),
                        color = "Verde",
                        placa = "AB123CM",
                        marca = "Toyota"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para registrar un vehiculo retorna true")]
        [ClassData(typeof(VehiculoClassData))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnTrue(VehiculoRegisterDTO vehiculo)
        {
            _serviceMockVehiculo
                .Setup(x => x.RegisterVehiculo(It.IsAny<Vehiculo>()))
                .Returns(1);
            int result = _logic.RegisterVehiculo(vehiculo);

            Assert.Equal(1,result);
            return Task.CompletedTask;
        }

        public class VehiculoClassDataInvalidfileds : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new VehiculoRegisterDTO()
                    {
                        anioModelo = 2003,
                        fechaCompra = new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local),
                        color = "esto no es un color",
                        placa = "AB123CM",
                        marca = "Toyota"
                    }
                };
                yield return new object[] {
                    new VehiculoRegisterDTO()
                    {
                        anioModelo = 2003,
                        fechaCompra = new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local),
                        color = "Verde",
                        placa = "Esta placa es muy larga",
                        marca = "Toyota"
                    }
                };
                yield return new object[] {
                    new VehiculoRegisterDTO()
                    {
                        anioModelo = 2003,
                        fechaCompra = new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local),
                        color = "Verde",
                        placa = "AB123CM",
                        marca = "esto no es una placa"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        
        [Theory (DisplayName ="Logic: Ejecuta la logica para registrar un vehiculo retorna RCVInvalidFieldException")]
        [ClassData(typeof(VehiculoClassDataInvalidfileds))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnRCVInvalidFieldException(VehiculoRegisterDTO vehiculo)
        {
            Assert.Throws<RCVInvalidFieldException>(() =>  _logic.RegisterVehiculo(vehiculo));
            return Task.CompletedTask;
        }
    

        [Fact (DisplayName ="Logic: Agrega asegurado a un Vehiculo retorna true")]
        public Task ShouldAseguradoTovehiculoReturnTrue()
        {
            Guid vehiculoId = new Guid();
            Guid aseguradoId = new Guid();

            _serviceMockVehiculo   
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO());
            _serviceMockAsegurado  
                .Setup(x => x.GetAseguradoByGuid(It.IsAny<Guid>()))
                .Returns(new AseguradoDTO()); 
            
            _serviceMockVehiculo
                .Setup(x => x.AddAsegurado(It.IsAny<Guid>(),It.IsAny<Guid>()))
                .Returns(1);   
                
            int result = _logic.AddAseguradoToVehiculo(vehiculoId,aseguradoId);

            Assert.Equal(1,result);
            return Task.CompletedTask;
        }


        [Fact (DisplayName ="Logic: Intenta agregar asegurado a vehiculo retorna RCVNullException vehiculo no existe")]
        public Task ShouldAseguradoTovehiculoReturnRCVNullExceptionVehiculoNull()
        {
            Guid vehiculoId = new Guid();
            Guid aseguradoId = new Guid();

            _serviceMockVehiculo   
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()));
            _serviceMockAsegurado  
                .Setup(x => x.GetAseguradoByGuid(It.IsAny<Guid>()))
                .Returns(new AseguradoDTO());   
            
            _serviceMockVehiculo
                .Setup(x => x.AddAsegurado(It.IsAny<Guid>(),It.IsAny<Guid>()))
                .Returns(1);
                
            Assert.Throws<RCVNullException>(() => _logic.AddAseguradoToVehiculo(vehiculoId,aseguradoId));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta agregar asegurado a vehiculo que ya tiene un asegurado Retorna RCVNAsociationException")]
        public Task ShouldAseguradoTovehiculoReturnRCVNAsociationException()
        {
            Guid vehiculoId = new Guid();
            Guid aseguradoId = new Guid();

            _serviceMockVehiculo   
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO{
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    asegurado  = new AseguradoDTO()
                }); 
            _serviceMockAsegurado  
                .Setup(x => x.GetAseguradoByGuid(It.IsAny<Guid>()))
                .Returns(new AseguradoDTO{
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001")
                });   
            
            _serviceMockVehiculo
                .Setup(x => x.AddAsegurado(It.IsAny<Guid>(),It.IsAny<Guid>()))
                .Returns(1);
                
            Assert.Throws<RCVAsociationException>(() => _logic.AddAseguradoToVehiculo(vehiculoId,aseguradoId));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta agregar asegurado a vehiculo retorna RCVNullException asegurado no existe")]
        public Task ShouldAseguradoTovehiculoReturnRCVNullExceptionAseguradoNull()
        {
            Guid vehiculoId = new Guid();
            Guid aseguradoId = new Guid();

            _serviceMockVehiculo   
                .Setup(x => x.GetVehiculoByGuid(It.IsAny<Guid>()))
                .Returns(new VehiculoDTO());
            _serviceMockAsegurado  
                .Setup(x => x.GetAseguradoByGuid(It.IsAny<Guid>()));
            
            _serviceMockVehiculo
                .Setup(x => x.AddAsegurado(It.IsAny<Guid>(),It.IsAny<Guid>()))
                .Returns(1);
                
            Assert.Throws<RCVNullException>(() => _logic.AddAseguradoToVehiculo(vehiculoId,aseguradoId));
            return Task.CompletedTask;
        }
    }
}
*/
