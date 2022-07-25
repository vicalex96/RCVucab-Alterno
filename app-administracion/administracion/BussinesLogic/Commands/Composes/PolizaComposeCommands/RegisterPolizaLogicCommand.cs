using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterPolizaLogicCommand: Command<int>
    {
        private int _result;
        private readonly PolizaRegisterDTO _polizaDTO;

        public RegisterPolizaLogicCommand(PolizaRegisterDTO polizaDTO)
        {
            _polizaDTO = polizaDTO;
        }

        
        public override void Execute()
        {
            try
            {
                // vehiculo tiene no puede tener polizas activas
                GetPolizaByVehiculoIdCommand getPolizaCommand = new GetPolizaByVehiculoIdCommand(_polizaDTO.vehiculoId);
                getPolizaCommand.Execute();
                
                if(getPolizaCommand.GetResult() != null)
                {
                    throw new RCVNullException("El vehiculo ya tiene una poliza registrada y activa");
                }

                // el vehiculo tiene que estar asignado a un asegurado
                GetVehiculoByIdCommand getVehiculoCommand = new GetVehiculoByIdCommand(_polizaDTO.vehiculoId);
                getVehiculoCommand.Execute();

                if(getVehiculoCommand.GetResult().asegurado == null)
                {
                    throw new RCVAsociationException("El vehiculo no tiene un asegurado asignado");
                }

                // El rango de fechas de la poliza debe ser correcto
                if(_polizaDTO.fechaVencimiento < _polizaDTO.fechaRegistro)
                {
                    throw new RCVDateOrderException("La fecha de vencimiento debe ser mayor a la fecha de registro");
                }

                RegisterPolizaCommand registerPolizaCommand = new RegisterPolizaCommand( _polizaDTO );
                registerPolizaCommand.Execute();
                _result = registerPolizaCommand.GetResult();
            }   
            catch(RCVNullException ex)
            {
                throw ex;
            }
            catch(RCVAsociationException ex)
            {
                throw ex;
            }
            catch(RCVDateOrderException ex)
            {
                throw ex;
            }
            catch(ArgumentException ex)
            {
                throw new RCVInvalidFieldException("El tipo de póliza no es valido", ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un problema al intentar registrar",ex);
            }

        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}