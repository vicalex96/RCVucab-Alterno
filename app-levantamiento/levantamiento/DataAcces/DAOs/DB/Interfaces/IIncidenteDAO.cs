
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.DataAccess.DAOs
{
    public interface IIncidenteDAO
    {
        public Guid RegisterIncidente(Incidente incidente);
        public ICollection<IncidenteToShowDTO> GetAll();
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud();

        public IncidenteDTO GetIncidenteById(Guid incidenteId);

    }
}