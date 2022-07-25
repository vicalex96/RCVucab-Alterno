using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterIncidenteLogicCommand: Command<int>
    {
        private int _result;
        private readonly IncidenteRegisterDTO _incidenteDTO;

        public RegisterIncidenteLogicCommand(IncidenteRegisterDTO incidenteDTO)
        {
            _incidenteDTO = incidenteDTO;
        }

        
        public override void Execute()
        {
            try
            {
                RegisterIncidenteCommand registerCommand = new RegisterIncidenteCommand( _incidenteDTO );
                registerCommand.Execute();
                _result = registerCommand.GetResult();
                
                /* _productorRabbit.SendMessage(
                    Routings.perito,
                    "registrar_incidente",
                    "Id-"+
                    incidente.Id.ToString()+
                    ":polizaId-"+
                    incidente.polizaId.ToString()
                );*/
                
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