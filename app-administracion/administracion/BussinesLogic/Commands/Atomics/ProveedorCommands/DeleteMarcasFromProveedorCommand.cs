using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class DeleteMarcasFromProveedorCommand: Command<int>
    {
        private int _result;
        private readonly Guid _proveedorId;

        public DeleteMarcasFromProveedorCommand( Guid proveedorId)
        {
            _proveedorId = proveedorId;
        }
        
        public override void Execute()
        {
            ProveedorDAO dao = DAOFactory.createProveedorDAO();
            _result = dao.DeleteMarcasFromProveedor(_proveedorId);
        }

        public override int  GetResult()
        {
            return _result!;
        }
    }
}