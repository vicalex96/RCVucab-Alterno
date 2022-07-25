using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetAseguradoByIdCommand: Command<AseguradoDTO>
    {
        private AseguradoDTO? _result;
        private readonly Guid _aseguradoId;

        public GetAseguradoByIdCommand(Guid aseguradoId)
        {
            _aseguradoId = aseguradoId;
        }

        
        public override void Execute()
        {
            AseguradoDAO dao = DAOFactory.createAseguradoDAO();
            _result = dao.GetAseguradoByGuid(_aseguradoId);

        }

        public override AseguradoDTO GetResult()
        {
            return _result!;
        }
    }
}