using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.QueueLogic
{
    public class IncidenteQueue: IIncidenteQueue
    {
        
        private IncidenteQueueDTO GetIncidenteQueueDTO(Guid incidenteId)
        {
            try
            {
                IncidenteQueueDTO incidente = new IncidenteQueueDTO
                {
                    Id = incidenteId
                };
                return incidente;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        
        public async Task<ICollection<IncidenteQueueDTO>> GetDataFromQueue()
        {
            try
            {
                ConsumerRabbit consumer = new ConsumerRabbit(
                    Exchanges.gerencia,
                    Routings.perito,
                    QueueNames.incidente
                    );
                ICollection<IncidenteQueueDTO> IncidenteList = new List<IncidenteQueueDTO>();

                List<string> messages = await consumer.startReceiverAdmin();
                foreach(string message in messages)
                {
                    Console.WriteLine($"-> Mensaje: {message}");
                    Guid incidenteId = Guid.Parse(message.Split(':')[1]);
                    Console.WriteLine($"Mensaje: {message} {incidenteId}");
                    try
                    {
                        IncidenteList.Add(
                        GetIncidenteQueueDTO(incidenteId)
                        );
                    }
                    catch(Exception ex)
                    {
                        messages.Remove(message);
                    }

                }

                return IncidenteList;
            }
            catch(Exception ex){
                throw new RCVException("Error al obtener los datos de la cola", ex);
            }
        }

        
    }
}