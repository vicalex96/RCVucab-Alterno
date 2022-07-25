using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.Mappers;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Commands
{
    public class RegisterSolicitudCommand: Command<Guid>
    {
        private Guid _result;
        private readonly SolicitudRepacionRegisterDTO _solicitudDTO;

        public RegisterSolicitudCommand(SolicitudRepacionRegisterDTO solicitudDTO)
        {
            _solicitudDTO = solicitudDTO;
        }

        public override void Execute()
        {
            SolcitudReparacionDAO dao = DAOFactory.createSolcitudReparacionDAO();
            _result = dao.RegisterSolicitud(
                        SolicitudReparacionMapper.MapToEntity(_solicitudDTO)
                    );
        }

        public override Guid GetResult()
        {
            return _result!;
        }
    }
}
