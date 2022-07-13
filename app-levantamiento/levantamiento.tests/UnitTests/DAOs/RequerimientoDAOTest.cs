
using Microsoft.Extensions.Logging;
using Moq;
using levantamiento.Persistence.DAOs;
using levantamiento.Persistence.Database;
using levantamiento.Test.DataSeed;
using Xunit;
using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections;


namespace administracion.Test.UnitTests.DAOs
{
    public class RequerimientoDAOhould
    {
        private readonly RequerimientoDAO _dao;
        private readonly Mock<ILevantamientoDBContext> _contextMock;
        private readonly Mock<ILogger<ParteDAO>> _mockLogger;
        public RequerimientoDAOhould()
        {
            _contextMock = new Mock<ILevantamientoDBContext>();
            _mockLogger = new Mock<ILogger<ParteDAO>>();

            _dao = new RequerimientoDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataSolcitudes();
        }


        [Theory(DisplayName = "DAO: Consultar toda la lista de requerimientos de una Solicitud de reparacion, la prueba devuelve un True")]
        [InlineData("0c5c3262-d5ef-46c7-0001-000000000000")]
        public Task GetListOfrequerimientosByIdSolicitudReturnTrue(Guid SolicitudId)
        {
            var result = _dao.GetRequerimientosBySolicitudId(SolicitudId);
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }

        [Theory(DisplayName = "DAO: Intanta consultar toda la lista de requerimientos de una Solicitud que no existe, la prueba devuelve un falso")]
        [InlineData("0c5c3262-d5ef-46c7-0001-000000000005")]
        public Task GetListOfrequerimientosByIdSolicitudReturnFalse(Guid SolicitudId)
        {
            var result = _dao.GetRequerimientosBySolicitudId(SolicitudId);
            var isEmpty = result.Any();
            Assert.False(isEmpty);
            return Task.CompletedTask;
        }
        


        public class RequerimientoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Requerimiento()
                    {
                        requerimientoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001"),
                        solicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000000"),
                        parteId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001"),
                        descripcion = "Piesa en mal estado con posibilidades de ser reparada",
                        tipoRequerimiento = TipoRequerimiento.Reparacion,
                        cantidad = 2

                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "DAO: Registra una parte de vehiculo al listdo del sistema, la prueba devuelve un True")]
        [ClassData(typeof(RequerimientoClassData))]
        public Task RegisterRegistraRequerimientoReturnTrue(Requerimiento requerimiento)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(0);
            var result = _dao.RegisterRequerimiento(requerimiento);
            Assert.True(result);
            return Task.CompletedTask;
        }
    }
}