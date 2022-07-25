using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetSolicitudByIncidenteIdCommand: Command<List<SolicitudesReparacionDTO>>
    {
        private List<SolicitudesReparacionDTO>? _result;
        private readonly Guid _incidenteId;

        public GetSolicitudByIncidenteIdCommand(Guid incidenteId)
        {
            _incidenteId = incidenteId;
        }

        
        public override void Execute()
        {
            SolcitudReparacionDAO dao = DAOFactory.createSolcitudReparacionDAO();
            _result = dao.GetSolicitudByIncidenteId(_incidenteId);

        }

        public override List<SolicitudesReparacionDTO> GetResult()
        {
            return _result!;
        }
    }
}