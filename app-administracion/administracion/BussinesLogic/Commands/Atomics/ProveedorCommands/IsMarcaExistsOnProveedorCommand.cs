using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class IsMarcaExistsOnProveedorCommand: Command<bool>
    {
        private bool _result;
        private readonly Guid _proveedorId;
        private readonly MarcaName _marca;

        public IsMarcaExistsOnProveedorCommand(Guid proveedorId, MarcaName marca)
        {
            _proveedorId = proveedorId;
            _marca = marca;
        }

        
        public override void Execute()
        {
            ProveedorDAO Dao = DAOFactory
                .createProveedorDAO();
            _result = Dao.IsMarcaExistsOnProveedor( _proveedorId, _marca );

        }
        
        public override bool GetResult()
        {
            return _result!;
        }
    }
}