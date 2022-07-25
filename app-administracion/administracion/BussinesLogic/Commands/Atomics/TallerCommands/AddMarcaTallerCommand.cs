using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class AddMarcaTallerCommand: Command<int>
    {
        private int _result;
        private readonly Guid _proveedorId;
        private readonly MarcaTaller _marca;

        public AddMarcaTallerCommand( MarcaTaller marca)
        {
            _marca = marca;
        }
        
        public override void Execute()
        {
            TallerDAO dao = DAOFactory.createTallerDAO();
            _result = dao.AddMarca( _marca );
        }

        public override int GetResult()
        {
            return _result!;
        }
    }
}