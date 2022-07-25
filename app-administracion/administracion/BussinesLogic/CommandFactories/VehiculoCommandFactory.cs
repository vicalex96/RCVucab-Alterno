using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.Commands
{
    public class VehiculoCommandFactory
    {
        public static AddAseguradoToVehiculoCommand createAddAseguradoToVehiculoCommand(Guid vehiculoId, Guid aseguradoId)
        {
            return new AddAseguradoToVehiculoCommand(vehiculoId, aseguradoId);
        }

        public static GetAllVehiculosCommand createGetAllVehiculosCommand()
        {
            return new GetAllVehiculosCommand();
        }
        public static GetVehiculoByIdCommand createGetVehiculoByIdCommand
        ( Guid vehiculoId )
        {
            return new GetVehiculoByIdCommand( vehiculoId );
        }

        public static GetVehiculosByAseguradoIdCommand createGetVehiculosByAseguradoIdCommand
        ( Guid aseguradoId)
        {
            return new GetVehiculosByAseguradoIdCommand( aseguradoId );
        }

        public static RegisterVehiculoCommand createRegisterVehiculoCommand( VehiculoRegisterDTO dto)
        {
            return new RegisterVehiculoCommand( dto );
        }

        public static AddAseguradoToVehiculoLogicCommand createAddAseguradoToVehiculoLogicCommand(Guid aseguradoId, Guid vehiculoId)
        {
            return new AddAseguradoToVehiculoLogicCommand(aseguradoId, vehiculoId);
        }

        public static RegisterVehiculoLogicCommand createRegisterVehiculoLogicCommand(VehiculoRegisterDTO registerDTO)
        {
            return new RegisterVehiculoLogicCommand( registerDTO);
        } 

    }
}