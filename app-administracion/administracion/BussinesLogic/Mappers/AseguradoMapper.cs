using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;

namespace administracion.BussinesLogic.Mappers
{
    public class AseguradoMapper
    {
        public static Asegurado MapToEntity(AseguradoDTO dto)
        {
            return new Asegurado 
            {
                Id = dto.Id,
                nombre = dto.nombre,
                apellido = dto.apellido,
            };
        }

        public static AseguradoDTO MapToRequestDTO (Asegurado entity)
        {
            return new AseguradoDTO
            {
                Id = entity.Id,
                nombre = entity.nombre,
                apellido = entity.apellido
            };
        }

        public static Asegurado MapToEntity( AseguradoRegisterDTO dto)
        {
            return new Asegurado 
            {
                Id = dto.Id,
                nombre = dto.nombre,
                apellido = dto.apellido,
            };
        }
    }
}