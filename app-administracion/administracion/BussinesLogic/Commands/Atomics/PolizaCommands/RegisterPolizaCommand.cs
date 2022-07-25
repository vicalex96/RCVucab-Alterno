using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterPolizaCommand: Command<int>
    {
        private int _result;
        private readonly PolizaRegisterDTO _polizadto;

        public RegisterPolizaCommand(PolizaRegisterDTO polizadto)
        {
            _polizadto = polizadto;
        }

        
        public override void Execute()
        {
            PolizaDAO dao = DAOFactory.createPolizaDAO();
            _result = dao.RegisterPoliza(
                PolizaMapper.MapToEntity(_polizadto)
            );
            
        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}