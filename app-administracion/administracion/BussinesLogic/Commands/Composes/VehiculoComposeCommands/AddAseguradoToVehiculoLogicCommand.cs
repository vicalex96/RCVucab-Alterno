using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class AddAseguradoToVehiculoLogicCommand: Command<int>
    {
        private int _result;
        private readonly Guid _aseguradoId;
        private readonly Guid _vehiculoId;

        public AddAseguradoToVehiculoLogicCommand(Guid vehiculoId,
        Guid aseguradoId )
        {
            _vehiculoId = vehiculoId;
            _aseguradoId = aseguradoId;
        }
        
        public override void Execute()
        {
            try
            {
                //El vehiculo tiene que existir
                GetVehiculoByIdCommand selectByIdCommand = new GetVehiculoByIdCommand(_vehiculoId);
                selectByIdCommand.Execute();
                VehiculoDTO vehiculoDTO = selectByIdCommand.GetResult();
                if( vehiculoDTO == null)
                {
                    throw new RCVNullException("El vehiculo no existe en el sistema");
                }
                
                //El vehiculo no puede tener registrado un asegurado
                if(vehiculoDTO.asegurado != null)
                    throw new RCVAsociationException("El vehiculo ya esta asignado a un asegurado");

                //el asegurado tiene que estar registrado
                GetAseguradoByIdCommand getAseguradoCommand = new GetAseguradoByIdCommand(_aseguradoId);
                getAseguradoCommand.Execute();
                if(getAseguradoCommand.GetResult() == null)
                    throw new RCVNullException("No existe ningun asegurado con dicho Id");
                
                AddAseguradoToVehiculoCommand registerCommand = new AddAseguradoToVehiculoCommand(_vehiculoId, _aseguradoId);
                
            }
            catch(RCVNullException ex)
            {
                throw ex;
            }
            catch(RCVAsociationException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un error desconocido",ex);
            }
        }
        
        public override int GetResult()
        {
            return _result!;
        }
    }
}