
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface ISolicitudReparacionDAO
    {
        public List<SolicitudesResparacionDTO> GetAll();
        public List<SolicitudesResparacionDTO> GetSolicitudWithoutTaller();
        public SolicitudesResparacionDTO GetSolicitudById(Guid solicitudId);
        public List<SolicitudesResparacionDTO> GetSolicitudByIncidenteId(Guid incidenteId);
        
        public bool RegisterSolicitud(SolicitudesRespacionRegisterDTO solicitudDTO);

        public bool SendNotificationsToQueue();
    }
}