using taller.BussinesLogic.DTOs;
using taller.Conections.rabbit;
using taller.Exceptions;

namespace taller.BussinesLogic.QueueLogic
{
    public interface ISolicitudQueue
    {
        public Task<ICollection<SolicitudQueueDTO>> GetDataFromQueue();

    }

    
}