using Microsoft.Extensions.Logging;
using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.BussinesLogic.LogicClasses;
using administracion.Conections.rabbit;
using administracion.Exceptions;
using administracion.Test.DataSeed;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class TallerLogicTest
    {
        private readonly TallerLogic _logic;
        private readonly Mock<ITallerDAO> _serviceMockTaller;
        private readonly Mock<IProductorRabbit> _serviceMockRabbit;

        private readonly Mock<IAdminDBContext> _contextMock;
        public TallerLogicTest()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _serviceMockTaller = new Mock<ITallerDAO>();
            _serviceMockRabbit = new Mock<IProductorRabbit>();
            _logic = new TallerLogic(_serviceMockTaller.Object, _serviceMockRabbit.Object);
        }

        public class TallerClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new TallerRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = "Taller 1"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Registra Taller y retorna True")]
        [ClassData(typeof(TallerClassData))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnTrue(TallerRegisterDTO taller)
        {
            _serviceMockTaller
                .Setup(x => x.RegisterTaller(It.IsAny<Taller>()))
                .Returns(It.IsAny<Guid>());
            var result = _logic.RegisterTaller(taller);

            Assert.True(result);
            return Task.CompletedTask;
        }

        public class TallerClassDataInvalidfileds : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new TallerRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = "string"
                    }
                };
                yield return new object[] {
                    new TallerRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = ""
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para registrar un Taller retorna RCVInvalidFieldException")]
        [ClassData(typeof(TallerClassDataInvalidfileds))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnRCVInvalidFieldException(TallerRegisterDTO taller)
        {
            Assert.Throws<RCVInvalidFieldException>(
                () =>  _logic.RegisterTaller(taller)
                );
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Registra marca a un taller y retorna True")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnTrue(string marca)
        {
            Guid tallerId = new Guid();
            _serviceMockTaller
                .Setup(x => x.IsMarcaExistsOnTaller(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(false);
            _serviceMockTaller
                .Setup(x => x.AddMarca(It.IsAny<MarcaTaller>()))
                .Returns(true);

            var result = _logic.AddMarca(tallerId,marca);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta Registrar una marca a un taller y retorna RCVInvalidFieldException")]
        public Task ShouldAddMarcaReturnRCVInvalidFieldException()
        {
            Guid tallerId = new Guid();
            String marca = "";

            Assert.Throws<RCVInvalidFieldException>(() => _logic.AddMarca(tallerId,marca));
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Intenta registrar una marca a un taller Retorna RCVAsociationException al ya estar registrada")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnRCVAsociationException(string marca)
        {
            Guid tallerId = new Guid();
            _serviceMockTaller
                .Setup(x => x.IsMarcaExistsOnTaller(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(true);

            Assert.Throws<RCVAsociationException>(() => _logic.AddMarca(tallerId,marca));
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Intenta registrar una marca a un taller Retorna RCVException ")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnRCVException(string marca)
        {
            Guid tallerId = new Guid();
            _serviceMockTaller
                .Setup(x => x.IsMarcaExistsOnTaller(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(false);
            _serviceMockTaller
                .Setup(x => x.AddMarca(It.IsAny<MarcaTaller>()))
                .Throws(new Exception(""));
            Assert.Throws<RCVException>(() => _logic.AddMarca(tallerId,marca));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Registra todas las marcas al Taller y retorna True")]
        public Task ShouldAddAllMarcasReturnTrue()
        {
            Guid tallerId = new Guid();
            _serviceMockTaller
                .Setup(x => x.DeleteMarcasFromTaller(It.IsAny<Guid>()));
            _serviceMockTaller
                .Setup(x => x.AddMarca(It.IsAny<MarcaTaller>()))
                .Returns(true);

            var result = _logic.AddAllMarcas(tallerId);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta registrar todas las marcas al Taller y retorna RCVException")]
        public Task ShouldAddAllMarcasReturnRCVException()
        {
            Guid tallerId = new Guid();
            _serviceMockTaller
                .Setup(x => x.DeleteMarcasFromTaller(It.IsAny<Guid>()));
            _serviceMockTaller
                .Setup(x => x.AddMarca(It.IsAny<MarcaTaller>()))
                .Throws(new Exception());

            Assert.Throws<RCVException>(() => _logic.AddAllMarcas(tallerId));
            return Task.CompletedTask;
        }

    }
}
