
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface ITallerLogic
    {
        public bool RegisterTaller (TallerRegisterDTO taller);

        public bool AddMarca(Guid tallerId, string marcaStr);
        public bool AddAllMarcas(Guid tallerId);
    }
}