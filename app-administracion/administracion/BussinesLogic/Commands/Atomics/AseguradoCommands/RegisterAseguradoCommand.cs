using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterAseguradoCommand: Command<int>
    {
        private int _result;
        private readonly AseguradoRegisterDTO _aseguradoDTO;

        public RegisterAseguradoCommand(AseguradoRegisterDTO aseguradoDTO)
        {
            _aseguradoDTO = aseguradoDTO;
        }
        
        public override void Execute()
        {
            AseguradoDAO dao = DAOFactory.createAseguradoDAO();
            _result = dao.RegisterAsegurado(
                        AseguradoMapper.MapToEntity(_aseguradoDTO)
                    );
        }

        public override int GetResult()
        {
            return _result!;
        }
    }
}