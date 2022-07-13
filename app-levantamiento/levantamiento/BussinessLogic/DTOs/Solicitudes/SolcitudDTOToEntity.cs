using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using levantamiento.Persistence.Entities;


namespace levantamiento.BussinesLogic.DTOs
{
    public static class SolicitudDTOToEntity
    {
        public static SolicitudReparacion ConvertDTOToEntity(SolicitudRepacionRegisterDTO solicitud)
        {
            return new SolicitudReparacion
            {
                SolicitudReparacionId = solicitud.Id,
                incidenteId = solicitud.incidenteId,
                vehiculoId = solicitud.vehiculoId
            };
        
        }
    }

}