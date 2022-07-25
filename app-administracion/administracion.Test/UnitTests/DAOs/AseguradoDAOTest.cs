using Microsoft.Extensions.Logging;
using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.Test.DataSeed;
using Xunit;
using administracion.Exceptions;
using  administracion.DataAccess.Entities;

namespace administracion.Test.UnitTests.DAOs
{
    public class AseguradoDAOShould
    {
        private readonly AseguradoDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<AseguradoDAO>> _mockLogger;

        public AseguradoDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _mockLogger = new Mock<ILogger<AseguradoDAO>>();

            _dao = new AseguradoDAO();
            _contextMock.SetupDbContextDataIncidenteProcess();
        }

        [Fact]
        public Task GetAsegurados_ReturnNotEmptyList()
        {
            var result = _dao.GetAsegurados();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar asegurados por Guid y retornar asegurado")]
        [InlineData("00000001-12aa-46bf-82a2-05ff65bb2c86")]
        public Task GetAseguradoByGuid_ReturnTrue(Guid aseguradoId)
        {
            var aseguradoDTO = _dao.GetAseguradoByGuid(aseguradoId);
            Assert.Equal(aseguradoId, aseguradoDTO.Id);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consultar Asegurado Por Nombre Y Apellido y retornar verdadero")]
        [InlineData("Juan", "Willson")]
        public Task GetAsegurado_PorNombreYApellido_ReturnTrue(string nombre, string apellido)
        {

            var aseguradoDTO = _dao.GetAseguradosPorNombreCompleto(nombre, apellido);
            var isNoEmpty = aseguradoDTO.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Registra un asegurado y retorna True")]
        public Task ShouldRegisterAseguradoReturnTrue()
        {
            Asegurado asegurado = new Asegurado();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(1);
            int result =_dao.RegisterAsegurado(asegurado);
            Assert.Equal(1,result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Intenta registra un Asegurado Retorna RCVException")]
        public Task ShouldTryRegisterAseguradoReturnException()
        {
            Asegurado asegurado = new Asegurado();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            Assert.Throws<RCVException>(() => _dao.RegisterAsegurado(asegurado));
            return Task.CompletedTask;
        }
    }
}