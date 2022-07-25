using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;
using levantamiento.Conections.APIs;
using levantamiento.BussinesLogic.Mappers;

namespace levantamiento.BussinesLogic.Logic
{
    public class SolicitudReparacionLogic: ISolicitudReparacionLogic
    {
        private readonly ISolicitudReparacionDAO _SolicitudReparacionDAO;
        private readonly IIncidenteAPI _incidenteAPI;
        private readonly IVehiculoAPI _vehiculoAPI;

        public SolicitudReparacionLogic(ISolicitudReparacionDAO SolicitudReparacionDAO,IIncidenteAPI incidenteAPI, IVehiculoAPI vehiculoAPI)
        {
            _SolicitudReparacionDAO = SolicitudReparacionDAO;
            _incidenteAPI = incidenteAPI;
            _vehiculoAPI = vehiculoAPI;
        }

        /// <summary>
        /// Revisa la solicitud a ver si cumple con la logica necesaria par poder ser registrado
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns> regresa un bool true si todo salio bien</returns>
        public async Task<bool> RegisterSolicitud(SolicitudRepacionRegisterDTO solicitud)
        {
            try
            {
                //verifica que exista el incidente
                if( await _incidenteAPI.GetIncidenteFromAdmin(solicitud.incidenteId) == null )
                    throw new RCVNullException("no se encontró ningun incidente asociado con el identificador indicado");

                //verifica que exista el vehiculo
                if( await _vehiculoAPI.GetVehiculoFromAdmin(solicitud.vehiculoId) == null )
                    throw new RCVNullException("no se encontró ningun vehiculo asociado con el identificador indicado");
                
                //registra la solicitud
                _SolicitudReparacionDAO.RegisterSolicitud(      
                    SolicitudReparacionMapper.MapToEntity(solicitud)
                );
                return true;
            }
            catch (RCVNullException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al ejecutar la logica para el registro de la solicitud", ex);
            }
        }

         ///Asocia un taller a una solicitud si cumple con los requisitos pedidos, le solicita al Taller que le regrese el mejor taller para la solicitud
        public bool AddTallerToSolicitud(Guid solicitudId)
        {
            return true;
        }
        
    }
}