using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetPolizaByIdCommand: Command<PolizaDTO>
    {
        private PolizaDTO? _result;
        private readonly Guid _polizaId;

        public GetPolizaByIdCommand( Guid polizaId)
        {
            _polizaId = polizaId;
        }
        
        public override void Execute()
        {
            PolizaDAO dao = DAOFactory.createPolizaDAO();
            _result = dao.GetPolizaById(_polizaId);
        }

        public override PolizaDTO GetResult()
        {
            return _result!;
        }
    }
}