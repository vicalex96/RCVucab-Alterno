
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace administracion.Persistence.DAOs
{
    public interface IProveedorDAO
    {
        public bool RegisterProveedor (ProveedorSimpleDTO proveedor);
        public ProveedorDTO GetProveedorByGuid (Guid proveedorId);
        public List<ProveedorDTO> GetProveedores();
        public bool AddMarca(Guid proveedorId,string marca, bool todasLasMarcas);
    }
}