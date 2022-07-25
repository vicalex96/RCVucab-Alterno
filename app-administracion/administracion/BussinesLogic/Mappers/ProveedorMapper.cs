using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Mappers
{
    public class ProveedorMapper
    {
        public static Proveedor MapToEntity(ProveedorDTO dto)
        {
            return new Proveedor 
            {
                Id = dto.Id,
                nombreLocal = dto.nombreLocal,
            };
        }
        
        public static Proveedor MapToEntity( ProveedorRegisterDTO dto)
        {
            Proveedor proveedor = new Proveedor();
            proveedor.nombreLocal = dto.nombreLocal;
            return proveedor;
            
        }

        public static ProveedorDTO MapToDTO (Proveedor entity)
        {
            return new ProveedorDTO
            {
                Id = entity.Id,
                nombreLocal = entity.nombreLocal,
            };
        }


    }
}