using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Logic
{
    public interface IIncidenteLogic
    {
        public int UpdateIncidenteRegisters();
        public Task<IncidenteDTO> GetDetailedIncidente (Guid IncidenteId);

        

    }

    
}