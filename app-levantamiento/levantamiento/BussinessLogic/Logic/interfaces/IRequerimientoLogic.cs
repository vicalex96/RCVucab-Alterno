using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Logic
{
    public interface IRequerimientoLogic
    {
        public bool RegisterRequerimiento(RequerimientoRegisterDTO requerimiento);
    }

}