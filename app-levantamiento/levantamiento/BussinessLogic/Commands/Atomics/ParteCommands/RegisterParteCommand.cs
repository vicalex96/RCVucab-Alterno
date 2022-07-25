using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.Mappers;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Commands
{
    public class RegisterParteCommand: Command<Guid>
    {
        private Guid _result;
        private readonly ParteRegisterDTO _parteDTO;

        public RegisterParteCommand(ParteRegisterDTO parteDTO)
        {
            _parteDTO = parteDTO;
        }

        public override void Execute()
        {
            ParteDAO dao = DAOFactory.createParteDAO();
            _result = dao.RegisterParte(
                        ParteMapper.MapToEntity(_parteDTO)
                    );
        }

        public override Guid GetResult()
        {
            return _result!;
        }
    }
}
