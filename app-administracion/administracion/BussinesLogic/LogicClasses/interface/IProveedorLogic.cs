
using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IProveedorLogic
    {
        public bool RegisterProveedor(ProveedorRegisterDTO proveedor);

        public bool AddMarca(Guid proveedorId, string marcaStr);
        public bool AddAllMarcas(Guid proveedorId);
    }
}