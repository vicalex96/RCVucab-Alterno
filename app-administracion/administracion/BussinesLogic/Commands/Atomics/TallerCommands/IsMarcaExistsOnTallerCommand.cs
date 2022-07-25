using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class IsMarcaExistsOnTallerCommand: Command<bool>
    {
        private bool _result;
        private readonly Guid _tallerId;
        private readonly MarcaName _marca;

        public IsMarcaExistsOnTallerCommand(Guid tallerId, MarcaName marca)
        {
            _tallerId = tallerId;
            _marca = marca;
        }

        
        public override void Execute()
        {
            TallerDAO Dao = DAOFactory
                .createTallerDAO();
            _result = Dao.IsMarcaExistsOnTaller( _tallerId, _marca );

        }
        
        public override bool GetResult()
        {
            return _result!;
        }
    }
}