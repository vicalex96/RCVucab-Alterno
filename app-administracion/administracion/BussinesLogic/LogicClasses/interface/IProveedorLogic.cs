
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IProveedorLogic
    {
        public int RegisterProveedor(ProveedorRegisterDTO proveedor);

        public int AddMarca(Guid proveedorId, string marcaStr);
        public int AddAllMarcas(Guid proveedorId);
    }
}