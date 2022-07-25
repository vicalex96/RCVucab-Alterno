using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterProveedorCommand: Command<int>
    {
        private int _result;
        private readonly ProveedorRegisterDTO _proveedorDTO;

        public RegisterProveedorCommand(ProveedorRegisterDTO proveedorDTO)
        {
            _proveedorDTO = proveedorDTO;
        }
        
        public override void Execute()
        {
            //registra el proveedor en el sistema
            ProveedorDAO dao = DAOFactory.createProveedorDAO();
            _result = dao.RegisterProveedor(
                    ProveedorMapper.MapToEntity(_proveedorDTO)
                );
        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}