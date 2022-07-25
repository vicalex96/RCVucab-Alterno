using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetProveedoresByGuidCommand: Command<ProveedorDTO>
    {
        private ProveedorDTO? _result;
        private readonly Guid _proveedorId;

        public GetProveedoresByGuidCommand( Guid proveedorId)
        {
            _proveedorId = proveedorId;
        }
        
        public override void Execute()
        {
            ProveedorDAO dao = DAOFactory.createProveedorDAO();
            _result = dao.GetProveedorByGuid(_proveedorId);
        }

        public override ProveedorDTO GetResult()
        {
            return _result!;
        }
    }
}