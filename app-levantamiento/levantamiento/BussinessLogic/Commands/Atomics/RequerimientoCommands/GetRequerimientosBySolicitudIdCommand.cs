using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;

namespace levantamiento.BussinesLogic.Commands
{
    public class GetRequerimientosBySolicitudIdCommand: Command<List<RequerimientoDTO>>
    {
        private List<RequerimientoDTO>? _result;
        private readonly Guid _solicitudId;

        public GetRequerimientosBySolicitudIdCommand(Guid solicitudId)
        {
            _solicitudId = solicitudId;
        }

        
        public override void Execute()
        {
            RequerimientoDAO dao = DAOFactory.createRequerimientoDAO();
            _result = dao.GetRequerimientosBySolicitudId(_solicitudId);

        }

        public override List<RequerimientoDTO> GetResult()
        {
            return _result!;
        }
    }
}