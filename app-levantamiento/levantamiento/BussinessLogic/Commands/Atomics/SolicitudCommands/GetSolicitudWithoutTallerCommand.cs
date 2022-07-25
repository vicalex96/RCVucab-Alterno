using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetSolicitudWithoutTallerCommand: Command<List<SolicitudesReparacionDTO>>
    {
        private List<SolicitudesReparacionDTO>? _result;
        public override void Execute()
        {
            SolcitudReparacionDAO dao = DAOFactory.createSolcitudReparacionDAO();
            _result = dao.GetSolicitudWithoutTaller();

        }

        public override List<SolicitudesReparacionDTO> GetResult()
        {
            return _result!;
        }
    }
}