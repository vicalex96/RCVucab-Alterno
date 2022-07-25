using Microsoft.Extensions.Logging;
using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using administracion.Exceptions;
using Xunit;
using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

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
            _mockLogger = new Mock<ILogger<ProveedorDAO>>();

            _dao = new ProveedorDAO();
            _contextMock.SetupDbContextDataEmpresas();
        }


        [Fact (DisplayName = "DAO: Obtener todos los Proveedores deberia retornar una lista")]
        public Task ShouldGetProveedoresReturnsProveedores()
        {
            var respuesta = _dao.GetProveedores();

            Assert.NotNull(respuesta);
            Assert.True(respuesta.Count() > 0);
            return Task.CompletedTask;
        }
        
        [Theory(DisplayName = "DAO: Consultar Proveedor según su Guid regresar un Proveedor")]
        [InlineData("100001c9-1212-46bf-82a3-05ff65bb2c86")]
        public Task ShouldGetProveedorByGuidReturnProveedor(Guid id)
        {
            ProveedorDTO Proveedor = _dao.GetProveedorByGuid(id);

            Assert.NotNull(Proveedor);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "DAO: Registra un Proveedor deberia regresar un Guid no vacio")]
        public Task ShouldRegisterProveedorReturnGuid()
        {
            Proveedor Proveedor = new Proveedor{
                Id = new Guid(),
                nombreLocal = "Proveedor 1",
            };
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(1);
            int result = _dao.RegisterProveedor(Proveedor);
            Assert.Equal(1,result);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "DAO: Al registrar un Proveedor genera una RCVExcepcion")]
        public Task ShouldRegisterProveedorRetrunRCVException()
        {
            Proveedor Proveedor = new Proveedor();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception(""));
            Assert.Throws<RCVException>(()=> _dao.RegisterProveedor(Proveedor));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Registrar una MarcaName para el Proveedor deberia retornar true ")]
        public Task ShouldAddMarcaToProveedorRetrunsTrue()
        {
            MarcaProveedor marca = new MarcaProveedor();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            int respuesta = _dao.AddMarca(marca);

            Assert.Equal(1,respuesta);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Intenta registrar marca devuelve RCVExcepcion ")]
        public Task ShouldAddMarcaToProveedorRetrunsRCVException()
        {
            MarcaProveedor marca = new MarcaProveedor();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _dao.AddMarca(marca));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Borra las marcas de un Proveedor regresa True ")]
        public Task ShouldDeleteMarcasFromProveedorRetrunsTrue()
        {
            Guid ProveedorId = new Guid();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            int respuesta = _dao.DeleteMarcasFromProveedor(ProveedorId);
            Assert.Equal(1,respuesta);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Borra las marcas de un Proveedor regresa RCVException ")]
        public Task ShouldDeleteMarcasFromProveedorRetrunsRCVException()
        {
            Guid ProveedorId = new Guid();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _dao.DeleteMarcasFromProveedor(ProveedorId));
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca existe en un Proveedor regresa True ")]
        [InlineData("200001c9-1212-46bf-82a3-05ff65bb2c87",MarcaName.Suzuki)]
        public Task ShouldGetExistingMarcaFromProveedorReturnTrue(Guid ProveedorId, MarcaName marca)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            bool respuesta = _dao.IsMarcaExistsOnProveedor(ProveedorId, marca);
            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca existe en un Proveedor con todas las marcas regresa True ")]
        [InlineData("200001c9-1212-46bf-82a3-05ff65bb2c87",MarcaName.Volkswagen)]
        public Task ShouldGetExistingMarcaFromProveedorWithAllMarcasReturnTrue(Guid ProveedorId, MarcaName marca)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            
            bool respuesta = _dao.IsMarcaExistsOnProveedor(ProveedorId, marca);
            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca no existe en un Proveedor regresa False ")]
        [InlineData("200001c9-1212-46bf-82a3-05ff65bb2c87",MarcaName.Toyota)]
        public Task ShouldGetExistingMarcaFromProveedorReturnFalse(Guid ProveedorId, MarcaName marca)
        {
            bool respuesta = _dao.IsMarcaExistsOnProveedor(ProveedorId, marca);
            Assert.False(respuesta);
            return Task.CompletedTask;
        }

    }
}