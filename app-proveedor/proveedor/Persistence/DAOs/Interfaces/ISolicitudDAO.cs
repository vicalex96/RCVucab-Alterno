
using proveedor.Persistence.Entities;
using proveedor.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace  administracion.DataAccess.DAOs
{
    public interface ISolicitudDAO
    {
        
        public string RegisterSolicitudesPorAPI(List<SolicitudDTO> solicitudes);
        
        public string RegisterRequerimientosPorAPI(List<RequerimientoDTO> requerimientos);
        
    }
}