using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.DAOs
{
    public class VehiculoDAOShould
    {
        private readonly VehiculoDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<VehiculoDAO>> _mockLogger;

        public VehiculoDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            
            _mockLogger = new Mock<ILogger<VehiculoDAO>>();

            _dao = new VehiculoDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataIncidenteProcess();
        }

        public class VehiculoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Vehiculo()
                    {
                        vehiculoId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        anioModelo = 2003,
                        fechaCompra = new DateTime(2022, 6, 22, 19, 25, 41, 41, DateTimeKind.Local),
                        color = Color.Verde,
                        placa = "AB123CM",
                        marca = Marca.Toyota
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

        [Fact(DisplayName = "DAO: Agregar Vehiculo y regresar un boleano true")]
        public Task shouldAddVehiculoReturnTrue()
        {
            Vehiculo vehiculo = new Vehiculo();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            var resultado = _dao.RegisterVehiculo(vehiculo);

            Assert.True(resultado);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Intenta agregar un Vehiculo y regresar una excepcion")]
        public Task shouldAddVehiculoReturnRCVException()
        {
            Vehiculo vehiculo = new Vehiculo();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception()); 

            Assert.Throws<RCVException>(() => _dao.RegisterVehiculo(vehiculo));
            return Task.CompletedTask;
        }

    
        [Theory(DisplayName = "asociar vehiculo con un asegurado retorna un boleano true")]
        [InlineData("00f401c9-12aa-46bf-82a3-05bb34bb2c03","00000001-12aa-46bf-82a2-05ff65bb2c86")]
        public Task shouldAssociatedAVehiculoWithAseguradoReturnTrue(Guid vehiculoId, Guid aseguradoId)
        {   
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);

            bool result = _dao.AddAsegurado(vehiculoId,aseguradoId);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "asociar vehiculo con un asegurado retorna una RCVException")]
        public Task shouldAssociatedAVehiculoWithAseguradoReturnRCVException()
        {   
            Vehiculo vehiculo = new Vehiculo();
            Guid aseguradoId = new Guid();
            Guid vehiculoId = new Guid();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception()); 

            Assert.Throws<RCVException>(() => _dao.AddAsegurado(vehiculoId,aseguradoId));
            return Task.CompletedTask;
        }

    }
}