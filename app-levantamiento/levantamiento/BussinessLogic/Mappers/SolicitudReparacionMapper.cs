using levantamiento.BussinesLogic.DTOs;
using  levantamiento.DataAccess.Entities;
using  levantamiento.DataAccess.Enums;

namespace levantamiento.BussinesLogic.Mappers
{
    public class SolicitudReparacionMapper
    {
        public static SolicitudReparacion MapToEntity( SolicitudesReparacionDTO dto)
        {
            return new SolicitudReparacion 
            {
                Id = dto.Id,
                incidenteId = dto.incidenteId,
                vehiculoId = dto.vehiculoId,
                tallerId = dto.tallerId,
            };
        }
        
        public static SolicitudReparacion MapToEntity( SolicitudRepacionRegisterDTO dto)
        {
            SolicitudReparacion solicitud = new SolicitudReparacion();
            solicitud.vehiculoId = dto.vehiculoId;
            solicitud.incidenteId = dto.incidenteId;
            return solicitud;
            
        }

        public static SolicitudesReparacionDTO MapToDTO (SolicitudReparacion entity)
        {
            return new SolicitudesReparacionDTO
            {
                Id = entity.Id,
                incidenteId = entity.incidenteId,
                vehiculoId = entity.vehiculoId,
                tallerId = entity.tallerId,
            };
        }


    }
}