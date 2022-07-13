using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;

namespace levantamiento.BussinesLogic.Logic
{
    public interface IParteLogic
    {
        public bool RegisterParte(ParteDTO parte);
    }

    
}