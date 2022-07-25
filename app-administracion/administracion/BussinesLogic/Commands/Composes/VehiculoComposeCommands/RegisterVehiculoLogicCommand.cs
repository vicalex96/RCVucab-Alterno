using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterVehiculoLogicCommand: Command<int>
    {
        private int _result;
        private readonly VehiculoRegisterDTO _vehiculoDTO;

        public RegisterVehiculoLogicCommand(VehiculoRegisterDTO vehiculoDTO)
        {
            _vehiculoDTO = vehiculoDTO;
        }
        
        public override void Execute()
        {
            try
            {
                if(_vehiculoDTO.placa.Count() >7)
                    throw new ArgumentException();

                RegisterVehiculoCommand registerCommand = new RegisterVehiculoCommand(_vehiculoDTO);
                registerCommand.Execute();
                _result = registerCommand.GetResult();
            }
            catch(ArgumentException ex)
            {
                throw new RCVInvalidFieldException("Error: alguno de los argumentos no es valido, color y marca deben de existir en el sistema, la placa tiene un maximo de 7 caracteres", ex);
            }
            catch (Exception e)
            {
                throw new RCVException("Error al registrar el incidente", e);
            }
        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}