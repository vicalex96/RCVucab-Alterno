using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Mappers
{
    public class TallerMapper
    {
        public static Taller MapToEntity(TallerDTO dto)
        {
            return new Taller 
            {
                Id = dto.Id,
                nombreLocal = dto.nombreLocal,
            };
        }
        
        public static Taller MapToEntity( TallerRegisterDTO dto)
        {
            Taller proveedor = new Taller();
            proveedor.nombreLocal = dto.nombreLocal;
            return proveedor;
            
        }

        public static TallerDTO MapToDTO (Taller entity)
        {
            return new TallerDTO
            {
                Id = entity.Id,
                nombreLocal = entity.nombreLocal,
            };
        }


    }
}