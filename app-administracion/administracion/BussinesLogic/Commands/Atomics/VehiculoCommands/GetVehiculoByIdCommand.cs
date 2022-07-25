using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetVehiculoByIdCommand: Command<VehiculoDTO>
    {
        private VehiculoDTO? _result;
        private readonly Guid _vehiculoId;

        public GetVehiculoByIdCommand( Guid vehiculoId)
        {
            _vehiculoId = vehiculoId;
        }
        
        public override void Execute()
        {
            VehiculoDAO dao = DAOFactory.createVehiculoDAO();
            _result = dao.GetVehiculoByGuid(_vehiculoId);
        }

        public override VehiculoDTO GetResult()
        {
            return _result!;
        }
    }
}