using Moq;
using levantamiento.DataAccess.DAOs;
using levantamiento.DataAccess.Database;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.Logic;
using levantamiento.Conections.rabbit;
using levantamiento.Conections.APIs;
using levantamiento.Exceptions;
using Xunit;
using System.Collections;

namespace administracion.Test.UnitTests.Logic
{
    public class ParteLogicTest
    {
        private readonly ParteLogic _logic;
        private readonly Mock<IParteDAO> _serviceMockParteDAO;



        public ParteLogicTest()
        {
            _serviceMockParteDAO = new Mock<IParteDAO>();
            _logic = new ParteLogic(_serviceMockParteDAO.Object);
        }

        
        public class ParteClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new ParteDTO()
                    {
                        Id = Guid.Parse("38f401c9-12aa-46bf-82a2-05ff65bb2600"),
                        nombre = "Puerta",
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Theory (DisplayName ="Logic: registra una parte y retorna True")]
        [ClassData(typeof(ParteClassData))]
        public Task ShouldExcuteRegisterParteReturnTrue(ParteDTO parte)
        {   
            _serviceMockParteDAO.Setup(x => x.RegisterParte(It.IsAny<Parte>()))
                .Returns(true);
            bool result = _logic.RegisterParte(parte);
            Assert.True(result);
            return Task.CompletedTask;
        }

        [Theory (DisplayName ="Logic: registra una parte y retorna Excepcion")]
        [ClassData(typeof(ParteClassData))]
        public Task ShouldExcuteRegisterParteReturnException(ParteDTO parte)
        {   
            _serviceMockParteDAO.Setup(x => x.RegisterParte(It.IsAny<Parte>()))
                .Throws(new Exception());
            
            Assert.Throws<RCVException>(() => _logic.RegisterParte(parte));
            return Task.CompletedTask;
        }

        [Fact (DisplayName ="Logic: intenta registra una parte y retorna RCVInvalidFieldException por nombre invalido")]
        public Task ShouldExcuteRegisterParteReturnRCVInvalidFieldException()
        {   
            ParteDTO parte = new ParteDTO();
            Assert.Throws<RCVInvalidFieldException>(() => _logic.RegisterParte(parte));
            return Task.CompletedTask;
        }

    }
}