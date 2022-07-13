using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;

namespace levantamiento.BussinesLogic.DTOs
{
    public class ParteDTO
    {
        public Guid Id {get; set;}
        public string nombre {get; set;} = "";
    }

}