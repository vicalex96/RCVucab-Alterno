
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace administracion.Persistence.DAOs
{
    public interface IIncidenteDAO
    {
        
        public IncidenteDTO consultarIncidente(Guid incidenteID);

        public List<IncidenteDTO> ConsultarIncidentesActivos();
        public bool RegisterIncidente (IncidenteSimpleDTO incidente);
        public bool actualizarIncidente(Guid incidenteId, EstadoIncidente estado);
    }
}