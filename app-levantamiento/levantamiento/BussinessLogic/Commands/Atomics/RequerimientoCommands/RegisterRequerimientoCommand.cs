using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.Mappers;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Commands
{
    public class RegisterRequerimientoCommand: Command<Guid>
    {
        private Guid _result;
        private readonly RequerimientoRegisterDTO _requerimientoDTO;

        public RegisterRequerimientoCommand(RequerimientoRegisterDTO requerimientoDTO)
        {
            _requerimientoDTO = requerimientoDTO;
        }

        public override void Execute()
        {
            RequerimientoDAO dao = DAOFactory.createRequerimientoDAO();
            _result = dao.RegisterRequerimiento(
                        RequerimientoMapper.MapToEntity(_requerimientoDTO)
                    );
        }

        public override Guid GetResult()
        {
            return _result!;
        }
    }
}
