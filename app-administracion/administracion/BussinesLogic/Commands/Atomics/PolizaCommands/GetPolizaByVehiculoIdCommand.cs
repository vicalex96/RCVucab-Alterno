using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetPolizaByVehiculoIdCommand: Command<PolizaDTO>
    {
        private PolizaDTO? _result;
        private readonly Guid _vehiculoId;

        public GetPolizaByVehiculoIdCommand( Guid vehiculoId )
        {
            _vehiculoId = vehiculoId;
        }
        
        public override void Execute()
        {
            PolizaDAO dao = DAOFactory.createPolizaDAO();
            _result = dao.GetPolizaByVehiculoId( _vehiculoId );
        }

        public override PolizaDTO GetResult()
        {
            return _result!;
        }
    }
}