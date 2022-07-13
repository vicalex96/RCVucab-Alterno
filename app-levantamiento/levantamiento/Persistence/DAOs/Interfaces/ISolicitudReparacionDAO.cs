
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface ISolicitudReparacionDAO
    {
        public List<SolicitudesReparacionDTO> GetAll();
        public List<SolicitudesReparacionDTO> GetSolicitudWithoutTaller();
        public SolicitudesReparacionDTO GetSolicitudById(Guid solicitudId);
        public List<SolicitudesReparacionDTO> GetSolicitudByIncidenteId(Guid incidenteId);
        
        public bool RegisterSolicitud(SolicitudReparacion solicitud);
        
    }
}