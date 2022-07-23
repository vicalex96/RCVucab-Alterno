using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Persistence.Enums;

namespace administracion.BussinesLogic.Mappers
{
    public class IncidenteMapper
    {
        public static Incidente MapToEntity(IncidenteDTO dto)
        {
            return new Incidente 
            {
                Id = dto.Id,
                polizaId = dto.polizaId,
                estadoIncidente = (EstadoIncidente)Enum.Parse(typeof(EstadoIncidente), dto.estadoIncidente)
            };
        }
        
        public static Incidente MapToEntity( IncidenteRegisterDTO dto)
        {
            Incidente incidente = new Incidente();
            incidente.polizaId = dto.polizaId;
            incidente.estadoIncidente = EstadoIncidente.Pendiente;
            incidente.fechaRegistrado = DateTime.Today;
            return incidente;
            
        }

        public static IncidenteDTO MapToDTO (Incidente entity)
        {
            return new IncidenteDTO
            {
                Id = entity.Id,
                polizaId = entity.polizaId,
                estadoIncidente = entity.estadoIncidente.ToString(),
            };
        }


    }
}