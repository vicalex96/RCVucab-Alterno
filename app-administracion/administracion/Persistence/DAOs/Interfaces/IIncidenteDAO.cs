
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;


namespace administracion.Persistence.DAOs
{
/// <summary>
    /// Interface para el listado de metodos de DAO de Incidentes
    /// </summary>
    public interface IIncidenteDAO
    {
        
        public IncidenteDTO consultarIncidente(Guid incidenteID);

        public List<IncidenteDTO> ConsultarIncidentesActivos();
        public bool RegisterIncidente (Incidente incidente);
        public bool actualizarIncidente(Guid incidenteId, EstadoIncidente estado);
    }
}