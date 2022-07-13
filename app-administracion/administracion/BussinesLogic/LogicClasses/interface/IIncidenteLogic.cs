
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Enums;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IIncidenteLogic
    {
        public int RegisterIncidente(IncidenteRegisterDTO asegurado);
        public int UpdateIncidenteState(Guid incidenteId, EstadoIncidente estado);

        public int RefreshIncidenteLogic();

    }
}