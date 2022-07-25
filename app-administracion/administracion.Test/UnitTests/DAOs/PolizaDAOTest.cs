using Microsoft.Extensions.Logging;
using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Test.DataSeed;
using Xunit;
using  administracion.DataAccess.Entities;
using administracion.Exceptions;

namespace administracion.Test.UnitTests.DAOs
{
    public class PolizaDAOShould
    {
        private readonly PolizaDAO _dao;
        private readonly Mock<IAdminDBContext> _contextMock;
        private readonly Mock<ILogger<PolizaDAO>> _mockLogger;

        public PolizaDAOShould()
        {
            _contextMock = new Mock<IAdminDBContext>();
            
            _mockLogger = new Mock<ILogger<PolizaDAO>>();

            _dao = new PolizaDAO();
            _contextMock.SetupDbContextDataIncidenteProcess();
        }

        [Theory(DisplayName = "DAO: Consulta la póliza activa por su ID retorna la póliza")]
        [InlineData("000401c9-12aa-46bf-82a2-05ff65bb2000")]
        public Task ShouldGetActivePolizaWithItsIdReturnPoliza(Guid polizaId)
        {

            PolizaDTO PolizaDTO = _dao.GetPolizaById(polizaId);
            Assert.NotNull(PolizaDTO);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Intenta consultar la póliza segn su ID regresa Null")]
        [InlineData("00f401c9-12aa-46bf-82a3-05bb34bb2c03")]
        public Task GetActivePolizaWithItsIdReturnNull(Guid polizaId)
        {

            PolizaDTO PolizaDTO = _dao.GetPolizaById(polizaId);
            Assert.Null(PolizaDTO);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Consulta la póliza activa por el ID de u vehiculo retorna la póliza")]
        [InlineData("00f401c9-12aa-46bf-82a3-05ff65bb2c00")]
        public Task ShouldGetActivePolizaFromVehiculoReturnPoliza(Guid vehiculoId)
        {

            PolizaDTO PolizaDTO = _dao.GetPolizaByVehiculoId(vehiculoId);
            Assert.NotNull(PolizaDTO);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Intenta consultar la poliza de un vehiculo regresa Null")]
        [InlineData("00f401c9-12aa-46bf-82a3-05bb34bb2c03")]
        public Task ShouldGetActivePolizaFromVehiculoReturnNull(Guid polizaId)
        {

            PolizaDTO PolizaDTO = _dao.GetPolizaByVehiculoId(polizaId);
            Assert.Null(PolizaDTO);
            return Task.CompletedTask;
        }


        [Fact(DisplayName = "DAO: Registra una póliza y regresa True")]
        public Task ShouldRegisterPolizaReturnTrue()
        {
            Poliza poliza = new Poliza();
            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(1);
            int result = _dao.RegisterPoliza(poliza);
            
            Assert.Equal(1,result);
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: Intenta registrar una póliza y regresa una RCVException")]
        public Task ShouldRegisterPolizaReturnException()
        {
            Poliza poliza = new Poliza();
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Throws(new Exception());
            var result = 
            
            Assert.Throws<RCVException>(() => _dao.RegisterPoliza(poliza));
            return Task.CompletedTask;
        }

    /*
        public bool RegisterPoliza (Poliza poliza);
        public PolizaDTO GetPolizaByGuid(Guid polizaId);
        public PolizaDTO GetPolizaByVehiculoGuid(Guid vehiculoID);
    */

    }
}
