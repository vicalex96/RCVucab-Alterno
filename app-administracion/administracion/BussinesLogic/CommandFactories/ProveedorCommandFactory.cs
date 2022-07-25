using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class ProveedorCommandFactory
    {
        public static AddMarcaProveedorCommand createAddMarcaProveedorCommand(MarcaProveedor marca)
        {
            return new AddMarcaProveedorCommand( marca);
        }

        public static DeleteMarcasFromProveedorCommand createDeleteMarcasFromProveedorCommand( Guid proveedorId )
        {
            return new DeleteMarcasFromProveedorCommand( proveedorId );
        }
        public static GetProveedoresByGuidCommand createGetProveedoresByGuidCommand( Guid proveedorId )
        {
            return new GetProveedoresByGuidCommand( proveedorId );
        }
        public static GetProveedoresCommand createGetProveedoresCommand()
        {
            return new GetProveedoresCommand();
        }

        public static IsMarcaExistsOnProveedorCommand  createIsMarcaExistsOnProveedorCommand( Guid proveedorId, MarcaName  marcaNombre )
        {
            return new IsMarcaExistsOnProveedorCommand(  proveedorId, marcaNombre );
        }

        public static RegisterProveedorCommand createRegisterProveedorCommand( ProveedorRegisterDTO dto )
        {
            return new RegisterProveedorCommand( dto );
        }

        public static AddAllMarcasProveedorLogicCommand createAddAllMarcasProveedorLogicCommand(Guid proveedorId)
        {
            return new AddAllMarcasProveedorLogicCommand(proveedorId);
        }

        public static AddMarcaProveedorLogicCommand createAddMarcaProveedorLogicCommand(Guid proveedorId, string marca)
        {
            return new AddMarcaProveedorLogicCommand( proveedorId,  marca);
        }

        public static RegisterProveedorLogicCommand  createRegisterProveedorLogicCommand(ProveedorRegisterDTO dto)
        {
            return new RegisterProveedorLogicCommand( dto );
        }
    }
}