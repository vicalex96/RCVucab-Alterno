
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;


namespace administracion.Persistence.DAOs
{
/// <summary>
    /// Interface para el listado de metodos de DAO de Incidentes
    /// </summary>
    public interface IIncidenteDAO
    {
        
        public IncidenteDTO GetIncidenteById(Guid incidenteID);
        public List<IncidenteDTO> GetIncidentesByState(EstadoIncidente estado);
        public bool RegisterIncidente (Incidente incidente);
        public bool UpdateIncidente(Incidente incidente);
    }
}