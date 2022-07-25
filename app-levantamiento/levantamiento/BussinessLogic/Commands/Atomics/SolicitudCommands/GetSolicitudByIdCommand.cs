using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetSolicitudByIdCommand: Command<SolicitudesReparacionDTO>
    {
        private SolicitudesReparacionDTO? _result;
        private readonly Guid _solicitudId;

        public GetSolicitudByIdCommand(Guid solicitudId)
        {
            _solicitudId = solicitudId;
        }

        
        public override void Execute()
        {
            SolcitudReparacionDAO dao = DAOFactory.createSolcitudReparacionDAO();
            _result = dao.GetSolicitudById(_solicitudId);

        }

        public override SolicitudesReparacionDTO GetResult()
        {
            return _result!;
        }
    }
}