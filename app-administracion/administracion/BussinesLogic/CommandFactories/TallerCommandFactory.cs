using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Commands;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class TallerCommandFactory
    {

        public static AddMarcaTallerCommand createAddMarcaTallerCommand(MarcaTaller marca)
        {
            return new AddMarcaTallerCommand( marca);
        }

        public static DeleteMarcasFromTallerCommand createDeleteMarcasFromTallerCommand( Guid proveedorId )
        {
            return new DeleteMarcasFromTallerCommand( proveedorId );
        }
        public static GetTallerByGuidCommand createGetTallerByGuidCommand( Guid proveedorId )
        {
            return new GetTallerByGuidCommand( proveedorId );
        }
        public static GetTalleresCommand createGetTalleresCommand()
        {
            return new GetTalleresCommand();
        }

        public static IsMarcaExistsOnTallerCommand  createIsMarcaExistsOnTallerCommand( Guid proveedorId, MarcaName  marcaNombre )
        {
            return new IsMarcaExistsOnTallerCommand(  proveedorId, marcaNombre );
        }

        public static RegisterTallerCommand createRegisterTallerCommand( TallerRegisterDTO dto )
        {
            return new RegisterTallerCommand( dto );
        }


        public static AddAllMarcasTallerLogicCommand createAddAllMarcasTallerLogicCommand(Guid TallerId)
        {
            return new AddAllMarcasTallerLogicCommand(TallerId);
        }

        public static AddMarcaTallerLogicCommand createAddMarcaTallerLogicCommand(Guid TallerId, string marca)
        {
            return new AddMarcaTallerLogicCommand( TallerId,  marca);
        }

        public static RegisterTallerLogicCommand  createRegisterTallerLogicCommand(TallerRegisterDTO dto)
        {
            return new RegisterTallerLogicCommand( dto );
        }
    }
}