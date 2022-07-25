using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetAllPartesCommand: Command<List<ParteDTO>>
    {
        private List<ParteDTO>? _result;
        public override void Execute()
        {
            ParteDAO dao = DAOFactory.createParteDAO();
            _result = dao.GetAll();

        }

        public override List<ParteDTO> GetResult()
        {
            return _result!;
        }
    }
}