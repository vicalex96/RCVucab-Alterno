using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.Mappers;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Commands
{
    public class RegisterIncidenteCommand: Command<Guid>
    {
        private Guid _result;
        private readonly IncidenteRegisterDTO _IncidenteDTO;

        public RegisterIncidenteCommand(IncidenteRegisterDTO IncidenteDTO)
        {
            _IncidenteDTO = IncidenteDTO;
        }

        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            _result = dao.RegisterIncidente(
                        IncidenteMapper.MapToEntity(_IncidenteDTO)
                    );
        }

        public override Guid GetResult()
        {
            return _result!;
        }
    }
}
