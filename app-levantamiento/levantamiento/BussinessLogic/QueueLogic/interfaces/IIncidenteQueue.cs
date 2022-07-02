using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.QueueLogic
{
    public interface IIncidenteQueue
    {
        public Task<ICollection<IncidenteQueueDTO>> GetDataFromQueue();

    }

    
}