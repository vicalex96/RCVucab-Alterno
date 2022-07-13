using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Conections.APIs;
using levantamiento.Persistence.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Logic
{
    public class IncidenteLogic: IIncidenteLogic
    {
        private readonly IConsumerRabbit _consumerRabbit;
        private readonly IIncidenteAPI _incidenteAPI;
        private readonly ISolicitudReparacionDAO _solicitudRepacionDAO;
        private readonly IIncidenteDAO _incidenteDAO;

        public IncidenteLogic(IConsumerRabbit consumerRabbit, IIncidenteDAO incidenteDAO,IIncidenteAPI incidenteAPI, ISolicitudReparacionDAO solicitudRepacionDAO)
        {
            _consumerRabbit = consumerRabbit;
            _incidenteAPI = incidenteAPI;
            _incidenteDAO = incidenteDAO;
            _solicitudRepacionDAO = solicitudRepacionDAO;
        }
        
        /// <summary>
        /// Obtiene el listado de incidentes de la cola y los registra en el sistema local de levantamiento
        /// </summary>
        /// <returns>Regresa la cantidad de incidentes registrados</returns>
        public int UpdateIncidenteRegisters()
        {
            try
            {
                //creamos una lista para guardar los incidentes
                ICollection<IncidenteDTO> IncidenteList = new List<IncidenteDTO>();
                //contador de registros realizados
                int total_register = 0;
                //configfura el consumidor para poder consumir la data
                _consumerRabbit.configureRabbit(
                    Exchanges.gerencia,
                    Routings.perito,
                    QueueNames.incidente
                );
                //Leemos la cola de incidentes y lo guardamos en una lista
                List<string> registers = _consumerRabbit.Consume();
                foreach(string register in registers)
                {
                    //transforma la data del mensaje a variables 
                    Guid incidenteId = Guid.Parse(register.Split(':')[1]);
                    Guid polizaId = Guid.Parse(register.Split(':')[2]);
                    
                    IncidenteDTO incidente = new IncidenteDTO();
                    incidente.Id = incidenteId;
                    incidente.polizaId = polizaId;
                    
                    // intenta registrar el incidente en el sistema 
                    bool result = _incidenteDAO.RegisterIncidente(
                        IncidenteDTOToEntity.ConvertDTOToEntity(incidente)
                    );
                        
                    if(result)
                        total_register++;
                }
                return total_register;
                
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al actualizar el listado de incidentes", ex);
            }
        }

        /// <summary>
        ///Solicita informacion detalla de un incidente
        /// </summary>
        /// <param name="incidenteId">Id del incidente</param>
        /// <returns>Regresa un incidente con sus solicitudes de reparacion</returns>
        public async Task<IncidenteDTO> GetDetailedIncidente (Guid incidenteId)
        {
            try
            {
                IncidenteDTO incidente = await _incidenteAPI.GetIncidenteFromAdmin(incidenteId);
                incidente.solicitudes = _solicitudRepacionDAO.GetSolicitudByIncidenteId(incidenteId);
                return incidente;
            }
            catch(RCVException ex)
            {
                throw new RCVException(ex.Mensaje, ex);
            }
            catch(Exception ex)
            {
                throw new Exception("Ocurrió un error desconocido", ex);
            }
        }
    }
}