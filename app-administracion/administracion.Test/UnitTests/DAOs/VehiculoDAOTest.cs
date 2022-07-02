//using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
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
            //var faker = new Faker();
            _contextMock = new Mock<IAdminDBContext>();
            // el Mock no emplea un DBcontext real en IAdminDBContext =>  obligamos una respuesta por defecto para el SaveChanges y de esta forma evitar un error al no tener un DBcontext real
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<VehiculoDAO>>();

            _dao = new VehiculoDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataVehiculo();
        }

        public class VehiculoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new VehiculoSimpleDTO()
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
            Assert.Equal(result.Count(), 3);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar vehiculos por Guid y retornar un VehiculoDTO")]
        [InlineData("26f401c9-12aa-46bf-82a3-05bb34bb2c03")]
        public Task shouldUseGuidForReturnVehiculo(Guid ID)
        {
            //Arrage
            var vehiculoDTO = _dao.GetVehiculoByGuid(ID);
            //Assert
            Assert.IsType<VehiculoDTO>(vehiculoDTO);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Agregar Vehiculo y regresar un mensaje de verifiacion")]
        [ClassData(typeof(VehiculoClassData))]
        public Task shouldAddVehiculoReturnMenssage(VehiculoSimpleDTO vehiculo)
        {
            var resultado = _dao.RegisterVehiculo(vehiculo);

            Assert.True(resultado);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "asociar vehiculo con un asegurado")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86", "3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public Task shouldAssociatedAVehiculoWithAseguradoReturnMessage(Guid vehiculoID, Guid aseguradoID)
        {
            bool result = _dao.AddAsegurado(vehiculoID, aseguradoID);
            Assert.True(result);
            return Task.CompletedTask;
        }
    }
}