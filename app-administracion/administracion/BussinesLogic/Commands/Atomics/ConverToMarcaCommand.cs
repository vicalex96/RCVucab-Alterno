using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class ConvertToMarcaCommand: Command<MarcaName>
    {
        private MarcaName _result;
        private readonly string _marcaName; 

        public ConvertToMarcaCommand(string color)
        {
            _marcaName = color;
        }
        public override void Execute()
        {
            _result = (MarcaName)Enum.Parse(typeof(MarcaName), _marcaName);
        }

        public override MarcaName GetResult()
        {
            return _result!;
        }
    }
}