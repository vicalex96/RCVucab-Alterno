using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.DAOs
{
    public class PolizaDAOShould
    {
        private readonly PolizaDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<PolizaDAO>> _mockLogger;

        public PolizaDAOShould()
        {
            //var faker = new Faker();
            _contextMock = new Mock<IAdminDBContext>();
            // el Mock no emplea un DBcontext real en IAdminDBContext =>  obligamos una respuesta por defecto para el SaveChanges y de esta forma evitar un error al no tener un DBcontext real
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<PolizaDAO>>();

            _dao = new PolizaDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataVehiculo();
        }

        public class PolizaClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new PolizaSimpleDTO
                    {
                        Id = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2100"),
                        fechaRegistro  = DateTime.ParseExact("20-10-2000","dd-MM-yyyy",null),
                        fechaVencimiento = DateTime.ParseExact("16-07-2005","dd-MM-yyyy",null),
                        tipoPoliza = "DaniosATerceros",
                        vehiculoId = new Guid("26f401c9-12aa-46bf-82a3-05bb34bb2c03")
                    },
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "DAO: registrar poliza y devolver mensaje")]
        [ClassData(typeof(PolizaClassData))]
        public Task ShouldRegisterPoliza(PolizaSimpleDTO poliza)
        {
            var result = _dao.RegisterPoliza(poliza);
            Assert.IsType<bool>(result);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar Polizas por Guid de vehiculo y retornar poliza actual")]
        [InlineData("26f401c9-12aa-46bf-82a3-05bb34bb3aa5")]
        public Task GetPoliza_PorID_ReturnTrue(Guid ID)
        {

            PolizaDTO PolizaDTO = _dao.GetPolizaByVehiculoGuid(ID);
            Assert.NotNull(PolizaDTO);
            return Task.CompletedTask;
        }


    }
}
