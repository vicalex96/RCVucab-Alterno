using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetProveedoresCommand: Command<List<ProveedorDTO>>
    {
        private List<ProveedorDTO>? _result;

        public override void Execute()
        {
            ProveedorDAO dao = DAOFactory.createProveedorDAO();
            _result = dao.GetProveedores();
        }

        public override List<ProveedorDTO> GetResult()
        {
            return _result!;
        }
    }
}