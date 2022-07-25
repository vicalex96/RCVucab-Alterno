using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.Commands
{
    public class PolizaCommandFactory
    {
        public static GetPolizaByIdCommand createGetPolizaByIdCommand( Guid incidenteId )
        {
            return new GetPolizaByIdCommand( incidenteId );
        }
        public static GetPolizaByVehiculoIdCommand createGetPolizaByVehiculoIdCommand( Guid incidenteId)
        {
            return new GetPolizaByVehiculoIdCommand( incidenteId);
        }
        public static RegisterPolizaCommand createRegisterPolizaCommand( PolizaRegisterDTO registerDTO )
        {
            return new RegisterPolizaCommand( registerDTO );
        }

        public static RegisterPolizaLogicCommand createRegisterPolizaLogicCommand( PolizaRegisterDTO registerDTO)
        {
            return new RegisterPolizaLogicCommand( registerDTO );
        }
    }
}