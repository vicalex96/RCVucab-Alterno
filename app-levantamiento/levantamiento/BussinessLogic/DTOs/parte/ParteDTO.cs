using levantamiento.Exceptions;
using levantamiento.DataAccess.Entities;

namespace levantamiento.BussinesLogic.DTOs
{
    public class ParteDTO
    {
        public Guid Id {get; set;}
        public string nombre {get; set;} = "";
    }

}