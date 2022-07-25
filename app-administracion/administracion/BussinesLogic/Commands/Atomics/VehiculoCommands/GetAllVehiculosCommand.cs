using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetAllVehiculosCommand: Command<List<VehiculoDTO>>
    {
        private List<VehiculoDTO>? _result;

        public override void Execute()
        {
            VehiculoDAO dao = DAOFactory.createVehiculoDAO();
            _result = dao.GetAllVehiculos();
        }

        public override List<VehiculoDTO> GetResult()
        {
            return _result!;
        }
    }
}