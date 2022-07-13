
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface ITallerLogic
    {
        public int RegisterTaller (TallerRegisterDTO taller);

        public int AddMarca(Guid tallerId, string marcaStr);
        public int AddAllMarcas(Guid tallerId);
    }
}