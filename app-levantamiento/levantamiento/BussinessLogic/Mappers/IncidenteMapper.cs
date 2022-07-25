using levantamiento.BussinesLogic.DTOs;
using  levantamiento.DataAccess.Entities;
using  levantamiento.DataAccess.Enums;

namespace levantamiento.BussinesLogic.Mappers
{
    public class IncidenteMapper
    {
        public static Incidente MapToEntity(IncidenteDTO dto)
        {
            return new Incidente 
            {
                Id = dto.Id,
                polizaId = dto.polizaId
            };
        }
        
        public static Incidente MapToEntity( IncidenteRegisterDTO dto)
        {
            Incidente incidente = new Incidente();
            incidente.polizaId = dto.polizaId;
            return incidente;
            
        }

        public static IncidenteDTO MapToDTO (Incidente entity)
        {
            return new IncidenteDTO
            {
                Id = entity.Id,
                polizaId = entity.polizaId
            };
        }


    }
}