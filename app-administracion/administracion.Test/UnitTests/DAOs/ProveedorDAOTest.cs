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

    public class ProveedorDAOShould
    {
        private readonly ProveedorDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<ProveedorDAO>> _mockLogger;

        public ProveedorDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<ProveedorDAO>>();

            _dao = new ProveedorDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataEmpresas();
        }

        [Fact(DisplayName = "DAO: Registrar un Proveedor deberia retornar true")]
        public Task ShouldRegisterProveedor()
        {
            ProveedorSimpleDTO proveedor = new ProveedorSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = "Proveedor 1"
            };
            bool respuesta = _dao.RegisterProveedor(proveedor);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Evita registra un Proveedor sin nombre")]
        public Task ShouldAvoidRegisterWithoutName()
        {
            ProveedorSimpleDTO proveedor = new ProveedorSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = ""
            };
            Assert.Throws<RCVException>(()=> _dao.RegisterProveedor(proveedor));
            return Task.CompletedTask;
        }
        [Fact(DisplayName = "DAO: Evita registra un Proveedor sin nombre")]
        public Task ShouldAvoidRegisterWithDefault()
        {
            ProveedorSimpleDTO proveedor = new ProveedorSimpleDTO()
            {
                Id = Guid.Parse("111101c9-1212-46bf-82a3-05ff65bb2100"),
                nombreLocal = "string"
            };
            Assert.Throws<RCVException>(()=> _dao.RegisterProveedor(proveedor));
            return Task.CompletedTask;
        }

        [Fact (DisplayName = "DAO: Obtener todos los Proveedores deberia retornar una lista")]
        public Task ShouldGetProveedores()
        {
            var respuesta = _dao.GetProveedores();

            Assert.NotNull(respuesta);
            Assert.True(respuesta.Count() > 0);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar Proveedor según su Guid regresar un Proveedor")]
        [InlineData("100001c9-1212-46bf-82a3-05ff65bb2c86")]
        public Task ShouldGetProveedorByGuid(Guid id)
        {
            ProveedorDTO Proveedor = _dao.GetProveedorByGuid(id);

            Assert.NotNull(Proveedor);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Registrar una Marca para el proveedor ")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87","Ferrari",false)]
        public Task ShouldRegisterMarcaOnProveedor(Guid id,string marca, bool todasLasMarcas)
        {
            bool respuesta = _dao.AddMarca(id,marca,todasLasMarcas);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Registrar todas las marcas")]
        [InlineData("200001c9-1212-46bf-82a3-05ff65bb2c87","",true)]
        public Task ShouldRegisterAllMarcas(Guid id,string marca, bool todasLasMarcas)
        {
            bool respuesta = _dao.AddMarca(id,marca,todasLasMarcas);

            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Evita registrar una marca repetida")]
        [InlineData("200001c9-1212-46bf-82a3-05ff65bb2c87","Suzuki",false)]
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