using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;

namespace administracion.BussinesLogic.Commands
{
    public class GetAseguradoByFullNameCommand: Command<List<AseguradoDTO>>
    {
        private List<AseguradoDTO>? _result;
        private readonly string _nombre;
        private readonly string _apellido;

        public GetAseguradoByFullNameCommand(string nombre,string apellido)
        {
            _nombre = nombre;
            _apellido = apellido;
        }

        
        public override void Execute()
        {
            AseguradoDAO dao = DAOFactory.createAseguradoDAO();
            _result = dao.GetAseguradosPorNombreCompleto(_nombre,_apellido);

        }

        public override List<AseguradoDTO> GetResult()
        {
            return _result!;
        }
    }
}