using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class DeleteMarcasFromTallerCommand: Command<int>
    {
        private int _result;
        private readonly Guid _tallerId;

        public DeleteMarcasFromTallerCommand( Guid tallerId)
        {
            _tallerId = tallerId;
        }
        
        public override void Execute()
        {
            TallerDAO dao = DAOFactory.createTallerDAO();
            _result = dao.DeleteMarcasFromTaller(_tallerId);
        }

        public override int  GetResult()
        {
            return _result!;
        }
    }
}