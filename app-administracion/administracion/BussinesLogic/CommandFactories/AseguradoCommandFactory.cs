using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.Commands
{
    public class AseguradoCommandFactory
    {
        public static GetAseguradosCommand createGetAseguradosCommand()
        {
            return new GetAseguradosCommand();
        }

        public static GetAseguradoByIdCommand createGetAseguradoByIdCommand
        ( Guid aseguradoId )
        {
            return new GetAseguradoByIdCommand( aseguradoId );
        }
        public static GetAseguradoByFullNameCommand createGetAseguradoByFullNameCommand 
        ( string nombre, string apellido)
        {
            return new GetAseguradoByFullNameCommand( nombre, apellido );
        }

        public static RegisterAseguradoCommand createRegisterAseguradoCommand
        ( AseguradoRegisterDTO dto)
        {
            return new RegisterAseguradoCommand( dto );
        }

        public static RegisterAseguradoLogicCommand createRegisterAseguradoLogicCommand( AseguradoRegisterDTO registerDTO)
        {
            return new RegisterAseguradoLogicCommand(registerDTO);
        }

    }
}