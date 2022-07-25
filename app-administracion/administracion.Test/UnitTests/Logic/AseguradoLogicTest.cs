using Moq;
using  administracion.DataAccess.DAOs;
using  administracion.DataAccess.Database;
using administracion.BussinesLogic.DTOs;
/*
using  administracion.DataAccess.Entities;
using administracion.DataAccess.DAOs.Logic;
using administracion.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class AseguradoLogicTest
    {
        private readonly AseguradoLogic _logic;
        private readonly Mock<IAseguradoDAO> _serviceMockAsegurado;
        public AseguradoLogicTest()
        {
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
                .Returns(1);
            
            int result = _logic.RegisterAsegurado(asegurado);
            Assert.Equal(1,result);
            return Task.CompletedTask;
        }
        public class AseguradoClassDataInvalidfileds : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        nombre = "Pepito",
                        apellido = "",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        nombre = "",
                        apellido = "Perez",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
                        nombre = "string",
                        apellido = "Perez",
                    }
                };
                yield return new object[] {
                    new AseguradoRegisterDTO()
                    {
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
*/
