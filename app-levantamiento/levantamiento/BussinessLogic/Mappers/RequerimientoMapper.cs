using levantamiento.BussinesLogic.DTOs;
using  levantamiento.DataAccess.Entities;
using  levantamiento.DataAccess.Enums;

namespace levantamiento.BussinesLogic.Mappers
{
    public class RequerimientoMapper
    {
        public static Requerimiento MapToEntity(RequerimientoDTO dto)
        {
            return new Requerimiento 
            {
                Id = dto.Id,
                solicitudReparacionId = dto.Id,
                parteId = dto.parteId,
                descripcion = dto.descripcion,
                tipoRequerimiento = (TipoRequerimiento)Enum.Parse(typeof(TipoRequerimiento), dto.tipoRequerimiento),
                cantidad = dto.cantidad,
            };
        }
        
        public static Requerimiento MapToEntity( RequerimientoRegisterDTO dto)
        {
            Requerimiento requerimiento = new Requerimiento();
            requerimiento.solicitudReparacionId = dto.solicitudId;
            requerimiento.parteId = dto.parteId;
            requerimiento.descripcion = dto.descripcion;
            requerimiento.tipoRequerimiento = (TipoRequerimiento)Enum.Parse(typeof(TipoRequerimiento), dto.tipoRequerimiento);
            requerimiento.cantidad = dto.cantidad;

            
            return requerimiento;
            
        }

        public static RequerimientoDTO MapToDTO (Requerimiento entity)
        {
            return new RequerimientoDTO 
            {
                Id = entity.Id,
                solicitudId = entity.Id,
                parteId = entity.parteId,
                descripcion = entity.descripcion,
                tipoRequerimiento =  entity.tipoRequerimiento.ToString(),
                cantidad = entity.cantidad,
            };
        }


    }
}