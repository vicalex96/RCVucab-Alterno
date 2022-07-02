
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IIncidenteDAO
    {
        public bool UpdateList(ICollection<IncidenteQueueDTO> dataList);
        public ICollection<IncidenteToShowDTO> GetAll();
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud();

        public ICollection <IncidenteDTO> GetIncidenteById(Guid incidenteId);

        public Task<IncidenteDTO> GetDetailedIncidente (Guid IncidenteId);
    }
}