
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Enums;


namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Proveedor
    /// </summary>
    public interface IProveedorDAO
    {
        public int RegisterProveedor (Proveedor proveedor);
        public ProveedorDTO GetProveedorByGuid (Guid proveedorId);
        public List<ProveedorDTO> GetProveedores();
        public int AddMarca(MarcaProveedor Marca);
        public int DeleteMarcasFromProveedor(Guid proveedorId);
        public bool IsMarcaExistsOnProveedor(Guid proveedorId, MarcaName marca);
    }
}