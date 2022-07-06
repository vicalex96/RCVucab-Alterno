
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IIncidenteLogic
    {
        public bool RegisterIncidente(IncidenteRegisterDTO asegurado);
        public bool actualizarIncidente(Guid incidenteId, EstadoIncidente estado);

    }
}