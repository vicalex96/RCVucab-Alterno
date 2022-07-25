using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetIncidenteByIdCommand: Command<IncidenteDTO>
    {
        private IncidenteDTO? _result;
        private readonly Guid _incidenteId;

        public GetIncidenteByIdCommand( Guid incidenteId)
        {
            _incidenteId = incidenteId;
        }
        
        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            _result = dao.GetIncidenteById(_incidenteId);

        }

        public override IncidenteDTO GetResult()
        {
            return _result!;
        }
    }
}