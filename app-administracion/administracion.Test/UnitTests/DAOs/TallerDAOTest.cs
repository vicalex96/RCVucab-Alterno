
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

    public class TallerDAOShould
    {
        private readonly TallerDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<TallerDAO>> _mockLogger;

        public TallerDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _mockLogger = new Mock<ILogger<TallerDAO>>();

            _dao = new TallerDAO();
            _contextMock.SetupDbContextDataEmpresas();
        }


        [Fact (DisplayName = "DAO: Obtener todos los Talleres deberia retornar una lista")]
        public Task ShouldGetTalleresReturnsTalleres()
        {
            var respuesta = _dao.GetTalleres();
            Assert.NotNull(respuesta);
            Assert.True(respuesta.Count() > 0);
            return Task.CompletedTask;
        }
        
        [Theory(DisplayName = "DAO: Consultar Taller según su Guid regresar un Taller")]
        [InlineData("100001c9-12aa-46bf-82a3-05ff65bb2c86")]
        public Task ShouldGetTallerByGuidReturnTaller(Guid id)
        {
            TallerDTO Taller = _dao.GetTallerByGuid(id);
            Assert.NotNull(Taller);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "DAO: Registra un taller deberia regresar un Guid no vacio")]
        public Task ShouldRegisterTallerReturnGuid()
        {
            Taller taller = new Taller{
                Id = new Guid(),
                nombreLocal = "Taller 1",
            };
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(1);
            int result = _dao.RegisterTaller(taller);
            Assert.NotNull(result);
            return Task.CompletedTask;
        }
        
        [Fact(DisplayName = "DAO: Al registrar un taller genera una RCVExcepcion")]
        public Task ShouldRegisterTallerRetrunRCVException()
        {
            Taller taller = new Taller();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception(""));;
            Assert.Throws<RCVException>(()=> _dao.RegisterTaller(taller));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Registrar una Marca para el taller deberia retornar true ")]
        public Task ShouldAddMarcaToTallerRetrunsTrue()
        {
            MarcaTaller marca = new MarcaTaller();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            int result = _dao.AddMarca(marca);

            Assert.Equal(1,result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Intenta registrar marca devuelve RCVExcepcion ")]
        public Task ShouldAddMarcaToTallerRetrunsRCVException()
        {
            MarcaTaller marca = new MarcaTaller();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _dao.AddMarca(marca));
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Borra las marcas de un Taller regresa True ")]
        public Task ShouldDeleteMarcasFromTallerRetrunsTrue()
        {
            Guid tallerId = new Guid();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            int result = _dao.DeleteMarcasFromTaller(tallerId);
            Assert.Equal(1,result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Borra las marcas de un Taller regresa RCVException ")]
        public Task ShouldDeleteMarcasFromTallerRetrunsRCVException()
        {
            Guid tallerId = new Guid();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _dao.DeleteMarcasFromTaller(tallerId));
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca existe en un Taller regresa True ")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87",MarcaName.Suzuki)]
        public Task ShouldGetExistingMarcaFromTallerReturnTrue(Guid tallerId, MarcaName marca)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            bool respuesta = _dao.IsMarcaExistsOnTaller(tallerId, marca);
            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca existe en un Taller con todas las marcas regresa True ")]
        [InlineData("100001c9-12aa-46bf-82a3-05ff65bb2c86",MarcaName.Toyota)]
        public Task ShouldGetExistingMarcaFromTallerWithAllMarcasReturnTrue(Guid tallerId, MarcaName marca)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            
            bool respuesta = _dao.IsMarcaExistsOnTaller(tallerId, marca);
            Assert.True(respuesta);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Revisa si una marca no existe en un Taller regresa False ")]
        [InlineData("200001c9-12aa-46bf-82a3-05ff65bb2c87",MarcaName.Renault)]
        public Task ShouldGetExistingMarcaFromTallerReturnFalse(Guid tallerId, MarcaName marca)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            bool respuesta = _dao.IsMarcaExistsOnTaller(tallerId, marca);
            Assert.False(respuesta);
            return Task.CompletedTask;
        }

    }
}