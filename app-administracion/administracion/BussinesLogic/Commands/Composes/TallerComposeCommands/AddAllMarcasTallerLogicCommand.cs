using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class AddAllMarcasTallerLogicCommand: Command<int>
    {
        private int _result;
        private readonly Guid _tallerId;

        public AddAllMarcasTallerLogicCommand(Guid tallerId)
        {
            _tallerId = tallerId;
        }
        
        public override void Execute()
        {
            try
            {
                //Borra los posible registros de marcas del taller
                DeleteMarcasFromTallerCommand deleteCommand = new DeleteMarcasFromTallerCommand(_tallerId);
                deleteCommand.Execute();

                //convierte los datos ingresados en un objeto MarcaTaller
                MarcaTaller marcaTaller = MarcaFactory.createMarcaTallerTodasLasMarcas(_tallerId);
            
                AddMarcaTallerCommand registerCommand = new AddMarcaTallerCommand(marcaTaller);
                registerCommand.Execute();
                _result = registerCommand.GetResult();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el incidente", ex);
            }
        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}