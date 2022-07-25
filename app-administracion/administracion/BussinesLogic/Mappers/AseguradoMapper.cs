using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;

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
        public static Asegurado MapToEntity( AseguradoRegisterDTO dto)
        {
            Asegurado asegurado = new Asegurado();
            asegurado.nombre = dto.nombre;
            asegurado.apellido = dto.apellido;
            return asegurado;
            
        }

        public static AseguradoDTO MapToDTO (Asegurado entity)
        {
            return new AseguradoDTO
            {
                Id = entity.Id,
                nombre = entity.nombre,
                apellido = entity.apellido
            };
        }


    }
}