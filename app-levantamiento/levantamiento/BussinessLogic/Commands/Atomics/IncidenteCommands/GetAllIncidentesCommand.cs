using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetAllIncidentesCommand: Command<ICollection<IncidenteToShowDTO>>
    {
        private ICollection<IncidenteToShowDTO>? _result;
        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            _result = dao.GetAll();

        }

        public override ICollection<IncidenteToShowDTO> GetResult()
        {
            return _result!;
        }
    }
}