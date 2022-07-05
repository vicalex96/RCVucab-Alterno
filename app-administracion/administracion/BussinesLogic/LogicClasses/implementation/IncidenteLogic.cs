using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.Conections.rabbit;

namespace administracion.BussinesLogic.LogicClasses
{
    public class IncidenteLogic: IIncidenteLogic
    {
        private readonly IIncidenteDAO _incidenteDAO;
        private readonly IProductorRabbit _productorRabbit;

        public IncidenteLogic (IIncidenteDAO incidenteDao, IProductorRabbit productorRabbit)
        {
            _incidenteDAO = incidenteDao;
            _productorRabbit = productorRabbit;
        }

        /// <summary>
        /// registra un Incidente en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="Incidente">DTO de registro con la data de incidente</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool RegisterIncidente(IncidenteRegisterDTO incidente)
        {
            try
            {
                Incidente incidenteEntity = new Incidente{
                    incidenteId = incidente.Id,
                    polizaId = incidente.polizaId,
                    estadoIncidente = EstadoIncidente.Pendiente,
                    fechaRegistrado = DateTime.Today,
                }; 
                bool response =_incidenteDAO.RegisterIncidente(incidenteEntity);
                if(response)
                {
                    _productorRabbit.SendMessage(
                        Routings.perito,
                        "registrar_incidente",
                        incidente.Id.ToString()
                    );
                }
                return response;
            }
            catch (Exception e)
            {
                throw new RCVException("Error al registrar el incidente", e);
            }
        }
    }
}