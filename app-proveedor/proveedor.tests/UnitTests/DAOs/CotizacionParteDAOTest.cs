//using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using proveedor.Persistence.DAOs;
using proveedor.Persistence.Database;
using proveedor.BussinesLogic.DTOs;
using proveedor.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace proveedor.Test.UnitTests.DAOs
{
    public class CotizacionParteDAOShould
    {
        private readonly CotizacionParteDAO _dao;
        private readonly Mock<IProveedorDbContext> _contextMock;
        private readonly Mock<ILogger<CotizacionParteDAO>> _mockLogger;

        public CotizacionParteDAOShould()
        {
            //var faker = new Faker();
            _contextMock = new Mock<IProveedorDbContext>();
            // el Mock no emplea un DBcontext real en IAdminDBContext =>  obligamos una respuesta por defecto para el SaveChanges y de esta forma evitar un error al no tener un DBcontext real
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<CotizacionParteDAO>>();

            _dao = new CotizacionParteDAO(_contextMock.Object);
             _contextMock.SetupDbContextData();
            //_contextMock.SetupDbContext();
            
        }

        public class CotizacionParteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new CotizacionParteDTO()
                    {
                        CotizacionParteId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c90"),
                        ProveedorId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c87"),
                        PrecioParteUnidad = 123,
                        FechaEntrega = new DateTime(2022,06,1),
                        estado = "Pendiente",
                        RequerimientoId = new Guid("38f401c9-12aa-46bf-82a2-05ff65bb2c91"),
                       // requerimientos = new List<Requerimiento>()
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

       
        [Theory(DisplayName = "DAO: Registrar una Cotizacion de Parte  deberia retornar un mensaje")]
        [ClassData(typeof(CotizacionParteClassData))]
        public Task ShouldRegisterCotPat(CotizacionParteDTO CotPat)
        {
            var respuesta = _dao.createCotizacionParte(CotPat);

            Assert.Equal("Cotizacion de Parte se ha registrado correctamente", respuesta);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "DAO: Consulta CotizacionParte  retorna verdadero")]
        public Task GetCotizacionPartesReturnTrue()
        {
            //Arrage
            var result = _dao.GetCotizacionPartes();
            //Act
            var isNoEmpty = result.Any();
            //Assert
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }


        
        [Theory(DisplayName = "DAO: Consulta COTPATsegun su estado retorna verdadero")]
        [InlineData(EstadoCotPt.Pendiente)]
        public Task ShouldGetCotizacionPartesByestado(EstadoCotPt estado)
        {
            //Arrage
            var result = _dao.GetCotizacionPartesByestado(estado);
            //Act
            var isNoEmpty = result.Any();
            //Assert
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

    }
}