using levantamiento.BussinesLogic.DTOs;
using  levantamiento.DataAccess.Entities;
using  levantamiento.DataAccess.Enums;

namespace levantamiento.BussinesLogic.Mappers
{
    public class ParteMapper
    {
        public static Parte MapToEntity(ParteDTO dto)
        {
            return new Parte 
            {
                Id = dto.Id,
                nombre = dto.nombre,
            };
        }
        
        public static Parte MapToEntity( ParteRegisterDTO dto)
        {
            Parte parte = new Parte();
            parte.nombre = dto.nombre;
            return parte;
        }

        public static ParteDTO MapToDTO (Parte entity)
        {
            return new ParteDTO
            {
                Id = entity.Id,
                nombre = entity.nombre
            };
        }


    }
}