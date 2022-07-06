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
    public class ProveedorLogicTest
    {
        private readonly ProveedorLogic _logic;
        private readonly Mock<IProveedorDAO> _serviceMockProveedor;
        private readonly Mock<IProductorRabbit> _serviceMockRabbit;

        private readonly Mock<IAdminDBContext> _contextMock;
        public ProveedorLogicTest()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _serviceMockProveedor = new Mock<IProveedorDAO>();
            _serviceMockRabbit = new Mock<IProductorRabbit>();
            _logic = new ProveedorLogic(_serviceMockProveedor.Object, _serviceMockRabbit.Object);
        }

        public class ProveedorClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new ProveedorRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = "Proveedor 1"
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Registra Proveedor y retorna True")]
        [ClassData(typeof(ProveedorClassData))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnTrue(ProveedorRegisterDTO Proveedor)
        {
            _serviceMockProveedor
                .Setup(x => x.RegisterProveedor(It.IsAny<Proveedor>()))
                .Returns(It.IsAny<Guid>());
            var result = _logic.RegisterProveedor(Proveedor);

            Assert.True(result);
            return Task.CompletedTask;
        }

        public class ProveedorClassDataInvalidfileds : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new ProveedorRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = "string"
                    }
                };
                yield return new object[] {
                    new ProveedorRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombreLocal = ""
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName ="Logic: Ejecuta la logica para registrar un Proveedor retorna RCVInvalidFieldException")]
        [ClassData(typeof(ProveedorClassDataInvalidfileds))]
        public Task ShouldExcuteRegisterVehiculoLogicReturnRCVInvalidFieldException(ProveedorRegisterDTO Proveedor)
        {
            Assert.Throws<RCVInvalidFieldException>(
                () =>  _logic.RegisterProveedor(Proveedor)
                );
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Registra marca a un Proveedor y retorna True")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnTrue(string marca)
        {
            Guid ProveedorId = new Guid();
            _serviceMockProveedor
                .Setup(x => x.IsMarcaExistsOnProveedor(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(false);
            _serviceMockProveedor
                .Setup(x => x.AddMarca(It.IsAny<MarcaProveedor>()))
                .Returns(true);

            var result = _logic.AddMarca(ProveedorId,marca);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta Registrar una marca a un Proveedor y retorna RCVInvalidFieldException")]
        public Task ShouldAddMarcaReturnRCVInvalidFieldException()
        {
            Guid ProveedorId = new Guid();
            String marca = "";

            Assert.Throws<RCVInvalidFieldException>(() => _logic.AddMarca(ProveedorId,marca));
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Intenta registrar una marca a un Proveedor Retorna RCVAsociationException al ya estar registrada")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnRCVAsociationException(string marca)
        {
            Guid ProveedorId = new Guid();
            _serviceMockProveedor
                .Setup(x => x.IsMarcaExistsOnProveedor(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(true);

            Assert.Throws<RCVAsociationException>(() => _logic.AddMarca(ProveedorId,marca));
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: Intenta registrar una marca a un Proveedor Retorna RCVException ")]
        [InlineData("BMW")]
        public Task ShouldAddMarcaReturnRCVException(string marca)
        {
            Guid ProveedorId = new Guid();
            _serviceMockProveedor
                .Setup(x => x.IsMarcaExistsOnProveedor(It.IsAny<Guid>(), It.IsAny<Marca>()))
                .Returns(false);
            _serviceMockProveedor
                .Setup(x => x.AddMarca(It.IsAny<MarcaProveedor>()))
                .Throws(new Exception(""));
            Assert.Throws<RCVException>(() => _logic.AddMarca(ProveedorId,marca));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Registra todas las marcas al Proveedor y retorna True")]
        public Task ShouldAddAllMarcasReturnTrue()
        {
            Guid ProveedorId = new Guid();
            _serviceMockProveedor
                .Setup(x => x.DeleteMarcasFromProveedor(It.IsAny<Guid>()));
            _serviceMockProveedor
                .Setup(x => x.AddMarca(It.IsAny<MarcaProveedor>()))
                .Returns(true);

            var result = _logic.AddAllMarcas(ProveedorId);

            Assert.True(result);
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: Intenta registrar todas las marcas al Proveedor y retorna RCVException")]
        public Task ShouldAddAllMarcasReturnRCVException()
        {
            Guid ProveedorId = new Guid();
            _serviceMockProveedor
                .Setup(x => x.DeleteMarcasFromProveedor(It.IsAny<Guid>()));
            _serviceMockProveedor
                .Setup(x => x.AddMarca(It.IsAny<MarcaProveedor>()))
                .Throws(new Exception());

            Assert.Throws<RCVException>(() => _logic.AddAllMarcas(ProveedorId));
            return Task.CompletedTask;
        }

    }
}
