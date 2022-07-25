using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class IncidenteCommandFactory
    {
        public static GetIncidenteByIdCommand createGetIncidenteByIdCommand( Guid incidenteId )
        {
            return new GetIncidenteByIdCommand( incidenteId );
        }
        public static GetIncidentesByStateCommand createGetIncidentesByStateCommand( EstadoIncidente state)
        {
            return new GetIncidentesByStateCommand( state);
        }

        public static RegisterIncidenteCommand createRegisterIncidenteCommand( IncidenteRegisterDTO dto )
        {
            return new RegisterIncidenteCommand( dto );
        }

        public static UpdateIncidenteLogicCommand createUpdateIncidenteLogicCommand(Guid incidenteID, EstadoIncidente state)
        {
            return new UpdateIncidenteLogicCommand( incidenteID, state );
        }

        public static RegisterIncidenteLogicCommand createRegisterIncidenteLogicCommand( IncidenteRegisterDTO registerDTO)
        {
            return new RegisterIncidenteLogicCommand( registerDTO );
        }

    }
}