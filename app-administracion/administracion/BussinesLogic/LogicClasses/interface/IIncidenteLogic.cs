
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IIncidenteLogic
    {
        public bool RegisterIncidente(IncidenteRegisterDTO asegurado);
        public bool UpdateIncidenteState(Guid incidenteId, EstadoIncidente estado);

        public int RefreshIncidenteLogic();

    }
}