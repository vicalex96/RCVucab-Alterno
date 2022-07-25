using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class AddAseguradoToVehiculoCommand: Command<int>
    {
        private int  _result;
        private readonly Guid _vehiculoId;
        private readonly Guid _aseguradoId;

        public AddAseguradoToVehiculoCommand( Guid vehiculoId, Guid aseguradoId)
        {
            _vehiculoId = vehiculoId;
            _aseguradoId = aseguradoId;
        }

        public override void Execute()
        {
            VehiculoDAO dao = DAOFactory.createVehiculoDAO();
            _result = dao.AddAsegurado(_vehiculoId,_aseguradoId);
        }

        public override int GetResult()
        {
            return _result!;
        }
    }
}