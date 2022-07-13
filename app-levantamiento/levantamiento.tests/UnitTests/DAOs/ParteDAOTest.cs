
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
    public class ParteDAOShould
    {
        private readonly ParteDAO _dao;
        private readonly Mock<ILevantamientoDBContext> _contextMock;
        private readonly Mock<ILogger<ParteDAO>> _mockLogger;
        public ParteDAOShould()
        {
            _contextMock = new Mock<ILevantamientoDBContext>();
            _mockLogger = new Mock<ILogger<ParteDAO>>();

            _dao = new ParteDAO(_contextMock.Object);
            _contextMock.SetupDbContextDataSolcitudes();
        }

        [Fact(DisplayName = "DAO: Consultar toda la lista de partes de un vehiculo, la prueba decuelve un True")]
        public Task GetListOfPartesReturnTrue()
        {
            var result = _dao.GetAll();
            var isNoEmpty = result.Any();
            Assert.True(isNoEmpty);
            return Task.CompletedTask;
        }
        
        [Theory(DisplayName = "DAO: Consulta los datos de una parte de vehiculo segun su Id, retorna un registro")]
        [InlineData("0c5c3262-d5ef-46c7-0002-000000000001")]
        public Task GetParteByIdReturnParte(Guid parteId)
        {
            ParteDTO result = _dao.GetParteById(parteId);
            Assert.NotNull(result);
            return Task.CompletedTask;
        } 

        [Theory(DisplayName = "DAO: Consulta los datos de una parte de vehiculo según su Id que no esta registrada retorna nulo")]
        [InlineData("0c5c3262-d5ef-46c7-0002-000000000005")]
        public Task GetParteByIdReturnNull(Guid parteId)
        {
            ParteDTO result = _dao.GetParteById(parteId);
            Assert.Null(result);
            return Task.CompletedTask;
        } 

        
        public class ParteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Parte()
                    {
                        parteId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000010"),
                        nombre = "asiento del conductor"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(DisplayName = "DAO: Registra una parte de vehiculo al listdo del sistema, la prueba devuelve un True")]
        [ClassData(typeof(ParteClassData))]
        public Task RegisterParteReturnTrue(Parte parte)
        {
            _contextMock.Setup(m => m.DbContext.SaveChanges())
                .Returns(0);
            var result = _dao.RegisterParte(parte);
            Assert.True(result);
            return Task.CompletedTask;
        }
    }
}