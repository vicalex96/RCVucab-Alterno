using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetIncidenteByIdCommand: Command<IncidenteDTO>
    {
        private IncidenteDTO? _result;
        private readonly Guid _IncidenteId;

        public GetIncidenteByIdCommand(Guid IncidenteId)
        {
            _IncidenteId = IncidenteId;
        }

        
        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            _result = dao.GetIncidenteById(_IncidenteId);

        }

        public override IncidenteDTO GetResult()
        {
            return _result!;
        }
    }
}