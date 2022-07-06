/*using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class VehiculoLogicTest
    {
        private readonly VehiculoDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<VehiculoDAO>> _mockLogger;

        public VehiculoDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<VehiculoDAO>>();

            _dao = new VehiculoDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataIncidenteProcess();
        }

        public class VehiculoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new VehiculoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
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

        [Fact(DisplayName = "DAO: Consulta vehiculos retorna la lista de vehiculos ")]
        public Task shouldReturnAVehiculosList()
        {
            var result = _dao.GetAllVehiculos();
            Assert.Equal(4, result.Count());
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar vehiculos por Guid y retornar un VehiculoDTO")]
        [InlineData("00f401c9-12aa-46bf-82a3-05bb34bb2c03")]
        public Task shouldUseGuidForReturnVehiculo(Guid vehiculoId)
        {
            //Arrage
            var vehiculoDTO = _dao.GetVehiculoByGuid(vehiculoId);
            //Assert
            Assert.IsType<VehiculoDTO>(vehiculoDTO);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Agregar Vehiculo y regresar un mensaje de verifiacion")]
        [ClassData(typeof(VehiculoClassData))]
        public Task shouldAddVehiculoReturnMenssage(VehiculoRegisterDTO vehiculo)
        {
            var resultado = _dao.RegisterVehiculo(vehiculo);

            Assert.True(resultado);
            return Task.CompletedTask;
        }


        [Theory(DisplayName = "asociar vehiculo con un asegurado")]
        [InlineData("00f401c9-12aa-46bf-82a3-05bb34bb2c03","00000001-12aa-46bf-82a2-05ff65bb2c86")]
        public Task shouldAssociatedAVehiculoWithAseguradoReturnTrue(Guid vehiculoId, Guid aseguradoId)
        {   
            bool result = _dao.AddAsegurado(vehiculo,aseguradoId);
            Assert.True(result);
            return Task.CompletedTask;
        }
    }
}

*/