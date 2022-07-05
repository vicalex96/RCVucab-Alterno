
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;


namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Proveedor
    /// </summary>
    public interface IProveedorDAO
    {
        public Guid RegisterProveedor (Proveedor proveedor);
        public ProveedorDTO GetProveedorByGuid (Guid proveedorId);
        public List<ProveedorDTO> GetProveedores();
        public bool AddMarca(MarcaProveedor Marca);
        public bool DeleteMarcasFromProveedor(Guid proveedorId);
        public bool IsMarcaExistsOnProveedor(Guid proveedorId, Marca marca);
    }
}