
using  administracion.DataAccess.Entities;
using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Enums;


namespace  administracion.DataAccess.DAOs
{
/// <summary>
    /// Interface para el listado de metodos de DAO de Incidentes
    /// </summary>
        public interface IIncidenteDAO
        {
        
        public IncidenteDTO GetIncidenteById(Guid incidenteID);
        public List<IncidenteDTO> GetIncidentesByState(EstadoIncidente estado);

        public int RegisterIncidente (Incidente incidente);
        public int UpdateIncidente(Incidente incidente);
        }
}