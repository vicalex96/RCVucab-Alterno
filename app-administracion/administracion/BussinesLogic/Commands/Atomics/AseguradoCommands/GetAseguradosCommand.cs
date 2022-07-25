using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetAseguradosCommand: Command<List<AseguradoDTO>>
    {
        private List<AseguradoDTO>? _result;
        
        public override void Execute()
        {
            AseguradoDAO dao = DAOFactory.createAseguradoDAO();
            _result = dao.GetAsegurados();

        }

        public override List<AseguradoDTO> GetResult()
        {
            return _result!;
        }
    }
}