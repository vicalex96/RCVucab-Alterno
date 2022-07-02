using taller.BussinesLogic.DTOs;
using taller.Conections.rabbit;
using taller.Exceptions;

namespace taller.BussinesLogic.QueueLogic
{
    public class SolicitudQueue: ISolicitudQueue
    {
        
        private SolicitudQueueDTO GetSolicitudQueueDTO(Guid solicitudId)
        {
            try
            {
                SolicitudQueueDTO solicitud = new SolicitudQueueDTO
                {
                    Id = solicitudId
                };
                return solicitud;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<ICollection<SolicitudQueueDTO>> GetDataFromQueue()
        {
            try
            {
                ConsumerRabbit consumer = new ConsumerRabbit(
                    Exchanges.levantamiento,
                    Routings.taller,
                    QueueNames.solicitud
                    );
                ICollection<SolicitudQueueDTO> solicitudList = new List<SolicitudQueueDTO>();

                List<string> messages = await consumer.startReceiverAdmin();
                foreach(string message in messages)
                {
                    Guid solicitudId = Guid.Parse(message.Split(':')[1]);
                    
                    solicitudList.Add(
                        GetSolicitudQueueDTO(solicitudId)
                        );
                }
                return solicitudList;
            }
            catch(Exception ex){
                throw new RCVException("Error al obtener los datos de la cola", ex);
            }
        }

    }
}