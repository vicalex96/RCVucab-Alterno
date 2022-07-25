using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class ConvertToColorCommand: Command<Color>
    {
        private Color _result;
        private readonly string _color; 

        public ConvertToColorCommand(string color)
        {
            _color = color;
        }
        public override void Execute()
        {
            _result = (Color)Enum.Parse(typeof(Color), _color);
        }

        public override Color GetResult()
        {
            return _result!;
        }
    }
}