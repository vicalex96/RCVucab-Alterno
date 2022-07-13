
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IIncidenteDAO
    {
        public bool RegisterIncidente(Incidente incidente);
        public ICollection<IncidenteToShowDTO> GetAll();
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud();

        public IncidenteDTO GetIncidenteById(Guid incidenteId);

    }
}