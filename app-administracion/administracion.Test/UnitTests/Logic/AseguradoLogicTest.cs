using Moq;
using administracion.Persistence.DAOs;
using administracion.Persistence.Database;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.BussinesLogic.LogicClasses;
using administracion.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class AseguradoLogicTest
    {
        private readonly AseguradoLogic _logic;
        private readonly Mock<IAseguradoDAO> _serviceMockAsegurado;
        private readonly Mock<IAdminDBContext> _contextMock;
        public AseguradoLogicTest()
        {
            _contextMock = new Mock<IAdminDBContext>();
            _serviceMockAsegurado = new Mock<IAseguradoDAO>();
            _logic = new AseguradoLogic(_serviceMockAsegurado.Object);
        }

        public class AseguradoClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "Pepito",
                        apellido = "Perez",
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName = "Logic: Ejecuta la lógica para registrar un Asegurado retorna True")]
        [ClassData(typeof(AseguradoClassData))]
        public Task ShouldRegisterAseguradoReturnTrue(AseguradoRegisterDTO asegurado)
        {
            _serviceMockAsegurado
                .Setup(x => x.RegisterAsegurado(It.IsAny<Asegurado>()))
                .Returns(true);
            
            bool result = _logic.RegisterAsegurado(asegurado);
            Assert.True(result);
            return Task.CompletedTask;
        }
        public class AseguradoClassDataInvalidfileds : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "Pepito",
                        apellido = "",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "",
                        apellido = "Perez",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "string",
                        apellido = "Perez",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "pepito",
                        apellido = "string",
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory (DisplayName = "Logic: Ejecuta la lógica para registrar un Asegurado retorna RCVInvalidFieldException")]
        [ClassData(typeof(AseguradoClassDataInvalidfileds))]
        public Task ShouldRegisterAseguradoReturnRCVInvalidFieldException(AseguradoRegisterDTO asegurado)
        {
            Assert.Throws<RCVInvalidFieldException>(
                () => _logic.RegisterAsegurado(asegurado)
            );
            return Task.CompletedTask;
        }
        
    }
}