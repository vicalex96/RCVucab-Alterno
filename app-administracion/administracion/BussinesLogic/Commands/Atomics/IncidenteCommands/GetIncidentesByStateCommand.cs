using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class GetIncidentesByStateCommand: Command<List<IncidenteDTO>>
    {
        private List<IncidenteDTO>? _result;
        private readonly EstadoIncidente _state;

        public GetIncidentesByStateCommand( EstadoIncidente state)
        {
            _state = state;
        }
        
        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            _result = dao.GetIncidentesByState(_state);

        }

        public override List<IncidenteDTO> GetResult()
        {
            return _result!;
        }
    }
}