using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetVehiculosByAseguradoIdCommand: Command<List<VehiculoDTO>>
    {
        private List<VehiculoDTO>? _result;
        private readonly Guid _AseguradoId;

        public GetVehiculosByAseguradoIdCommand( Guid aseguradoId)
        {
            _AseguradoId = aseguradoId;
        }
        
        public override void Execute()
        {
            VehiculoDAO dao = DAOFactory.createVehiculoDAO();
            _result = dao.GetVehiculosByAsegurado(_AseguradoId);
        }

        public override List<VehiculoDTO> GetResult()
        {
            return _result!;
        }
    }
}