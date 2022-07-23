using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.Conections.rabbit;
using administracion.Persistence.Enums;
using administracion.BussinesLogic.Mappers;

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
        public int RegisterIncidente(IncidenteRegisterDTO incidente)
        {
            try
            {
                
                int response =_incidenteDAO.RegisterIncidente(
                    IncidenteMapper.MapToEntity(incidente)
                    );
                
                /* _productorRabbit.SendMessage(
                    Routings.perito,
                    "registrar_incidente",
                    "Id-"+
                    incidente.Id.ToString()+
                    ":polizaId-"+
                    incidente.polizaId.ToString()
                );*/
                

                return response;
            }
            catch (Exception e)
            {
                throw new RCVException("Error al registrar el incidente", e);
            }
        }

        public int UpdateIncidenteState(Guid incidenteId, EstadoIncidente estado)
        {
            try
            {
                //Revisa que existe el incidente
                IncidenteDTO incidente =_incidenteDAO.GetIncidenteById(incidenteId);
                
                if(incidente == null)
                    throw new RCVNullException("No existe ningun incidente con el id suministrado");

                incidente.estadoIncidente = estado.ToString();

                return _incidenteDAO.UpdateIncidente(
                        IncidenteMapper.MapToEntity(incidente)
                        );

            }
            catch (RCVNullException ex)
            {
                throw ex;
            }
            catch (RCVUpdateException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RCVException("No se pudo actualizar el incidente", ex);
            }
        }

        /// <summary>
        //Refresa la cola con los incidentes pendientes de ser atendidos
        /// </summary>
        /// <returns>cantidad de incidentes pendientes enviados a la cola</returns>
        public int RefreshIncidenteLogic()
        {
            try
            {
                int counter = 0;
                List<IncidenteDTO> incidentes = _incidenteDAO.GetIncidentesByState(EstadoIncidente.Pendiente);

                foreach (IncidenteDTO incidente in incidentes)
                {
                    bool response = _productorRabbit.SendMessage(
                        Routings.perito,
                        "registrar_incidente",
                        "Id-"+
                        incidente.Id.ToString()+
                        ":polizaId-"+
                        incidente.polizaId.ToString()
                    );
                    counter = (response == true)? counter++ : counter;
                }
                return counter;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al refrescar la cola de incidentes", ex);
            }
        }

    }
}