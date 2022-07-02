//using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using administracion.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.DAOs
{

    public class TallerDAOShould
    {
        private readonly TallerDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<TallerDAO>> _mockLogger;

        public TallerDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<TallerDAO>>();

            _dao = new TallerDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataEmpresas();
        }

        [Fact(DisplayName = "DAO: Registrar un Taller deberia retornar true")]
        public Task ShouldRegisterTaller()
        {
            TallerSimpleDTO taller = new TallerSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = "Taller 1"
            };
            bool respuesta = _dao.RegisterTaller(taller);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Evita registra un Taller sin nombre")]
        public Task ShouldAvoidRegisterWithoutName()
        {
            TallerSimpleDTO taller = new TallerSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = ""
            };
            Assert.Throws<RCVException>(()=> _dao.RegisterTaller(taller));
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "DAO: Evita registra un Taller sin nombre")]
        public Task ShouldAvoidRegisterWithDefault()
        {
            TallerSimpleDTO taller = new TallerSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = "string"
            };
            Assert.Throws<RCVException>(()=> _dao.RegisterTaller(taller));
            return Task.CompletedTask;
        }

        
        [Fact (DisplayName = "DAO: Obtener todos los Talleres deberia retornar una lista")]
        public Task ShouldGetTalleres()
        {
            var respuesta = _dao.GetTalleres();

            Assert.NotNull(respuesta);
            Assert.True(respuesta.Count() > 0);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar Taller según su Guid regresar un Taller")]
        [InlineData("100001c9-12aa-46bf-82a3-05ff65bb2c86")]
        public Task ShouldGetTallerByGuid(Guid id)
        {
            TallerDTO Taller = _dao.GetTallerByGuid(id);

            Assert.NotNull(Taller);
            return Task.CompletedTask;
        }
        
        [Theory(DisplayName = "DAO: Registrar una Marca para el taller ")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87","Ferrari",false)]
        public Task ShouldRegisterMarcaOnTaller(Guid id,string marca, bool todasLasMarcas)
        {
            bool respuesta = _dao.AddMarca(id,marca,todasLasMarcas);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Registrar todas las marcas")]
        [InlineData("100001c9-12aa-46bf-82a3-05ff65bb2c86","",true)]
        public Task ShouldRegisterAllMarcas(Guid id,string marca, bool todasLasMarcas)
        {
            bool respuesta = _dao.AddMarca(id,marca,todasLasMarcas);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Evita registrar una marca repetida")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87","General_Motors",false)]
        public Task ShouldAvoidRegisterRepitedMarca(Guid id,string marca, bool todasLasMarcas)
        {
            Assert.Throws<RCVException>(() => _dao.AddMarca(id,marca,todasLasMarcas));
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Evita registrar una marca repetida")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87","fdsfsdd",false)]
        public Task ShouldAvoidRegisterNoMarca(Guid id,string marca, bool todasLasMarcas)
        {

            Assert.Throws<RCVException>(() => _dao.AddMarca(id,marca,todasLasMarcas));
            return Task.CompletedTask;
        }
    }
}