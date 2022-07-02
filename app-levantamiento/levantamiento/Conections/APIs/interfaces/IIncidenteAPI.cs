using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Responses;

namespace levantamiento.Conections.APIs
{
    public interface IIncidenteAPI
    {
        public Task<IncidenteDTO> GetIncidenteFromAdmin(Guid incidenteId);
    }
}