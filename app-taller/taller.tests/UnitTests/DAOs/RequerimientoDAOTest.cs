//using Bogus;
using Microsoft.Extensions.Logging;
using Moq;
using taller.Persistence.DAOs;
using taller.Persistence.Database;
using taller.BussinesLogic.DTOs;
using taller.Test.DataSeed;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Collections;
using taller.Exceptions;
using taller.Persistence.Entities;

namespace taller.Test.UnitTests.DAOs
{
    public class RequerimientoDAOShould
    {
        private readonly RequerimientoDAO _dao;
        private readonly Mock<ITallerDBContext> _contextMock;
        private readonly Mock<ILogger<RequerimientoDAO>> _mockLogger;

        public RequerimientoDAOShould()
        {
            _contextMock = new Mock<ITallerDBContext>();

            _contextMock.Setup(m => m.DbContext.SaveChanges()).Returns(0);
            _mockLogger = new Mock<ILogger<RequerimientoDAO>>();

            _dao = new RequerimientoDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataEmpresas();
        }

        [Theory(DisplayName = "DAO: Consulta Los Requerimientos de una solicitud regresando una lista de requerimientos")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86")]
        public Task GetRequerimientosReturnTrue(Guid solicitudId)
        {
            var data = _dao.GetRequerimientos(solicitudId);
            var result = data.Any();
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Actualiza el tipo de requerimiento de un requerimiento")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86", TipoRequerimiento.Reparacion)]  
        public Task UpdateTipoRequerimientoReturnTrue(Guid requerimientoId, TipoRequerimiento tipo)
        {
            var result = _dao.UpdateTipoRequerimiento(requerimientoId, tipo);
            Assert.True(result);
            return Task.CompletedTask;
        }

        //
        [Theory(DisplayName = "DAO: Actualiza el tipo de requerimiento de un requerimiento regresa una excepcion")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86", TipoRequerimiento.Reparacion)]
        public Task UpdateTipoRequerimientoReturnException(Guid requerimientoId, TipoRequerimiento tipo)
        {
            Assert.Throws<RCVException>(
                () => _dao.UpdateTipoRequerimiento(requerimientoId, tipo)
            );
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Actualiza el tipo de requerimiento de un requerimiento")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86", 1000)]  
        public Task UpdateQuantityPiecesRequerimiento(Guid requerimientoId, int monto)
        {
            var result = _dao.UpdateQuantityPiecesRequerimiento(requerimientoId, monto);
            Assert.True(result);
            return Task.CompletedTask;
        }

        //
        [Theory(DisplayName = "DAO: Actualiza el tipo de requerimiento de un requerimiento regresa una excepcion")]
        [InlineData("38f401c9-12aa-46bf-82a2-05ff65bb2c86", -1)]
        public Task UpdateQuantityPiecesReqReturnException(Guid requerimientoId, int monto)
        {
            Assert.Throws<RCVException>(
                () => _dao.UpdateQuantityPiecesRequerimiento(requerimientoId, monto)
            );
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: registra un requerimiento retorna true")] 
        public Task RegisterRequerimientoReturnTrue()
        {
            var requerimiento = new RequerimientoDTO
            {
                Id = Guid.NewGuid(),
                solicitudRepId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                parteId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2c90"),
                descripcion = "hola",
                tipoRequerimiento = TipoRequerimiento.Reparacion.ToString(),
                cantidad = 1000,
                estado = EstadoRequerimiento.PorEntregar.ToString(),
            };
            var result = _dao.RegisterRequerimiento(requerimiento);
            Assert.True(result);
            return Task.CompletedTask;
        }

        //
        [Fact(DisplayName = "DAO: intenta regristrar requerimiento retorna excepcion por tipo requrimiento invalido")]
        public Task RegisterRequerimientoReturnExceptionPorTipo()
        {
            var requerimiento = new RequerimientoDTO
            {
                solicitudRepId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                tipoRequerimiento = EstadoCotRep.Facturado.ToString(),
                cantidad = 1000
            };
            Assert.Throws<RCVException>(
                () => _dao.RegisterRequerimiento(requerimiento)
            );
            return Task.CompletedTask;
        }

        [Fact(DisplayName = "DAO: intenta regristrar requerimiento retorna excepciono por cantidad invalida")]
        public Task RegisterRequerimientoReturnExceptionporCantidad()
        {
            var requerimiento = new RequerimientoDTO
            {
                solicitudRepId = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2c86"),
                tipoRequerimiento = EstadoCotRep.Facturado.ToString(),
                cantidad = -100
            };
            Assert.Throws<RCVException>(
                () => _dao.RegisterRequerimiento(requerimiento)
            );
            return Task.CompletedTask;
        }
    }
}