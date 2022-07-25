using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Mappers
{
    public class PolizaMapper
    {
        public static Poliza MapToEntity(PolizaDTO dto)
        {
            return new Poliza 
            {
                Id = dto.Id,
                fechaRegistro = dto.fechaRegistro,
                fechaVencimiento = dto.fechaVencimiento,
                tipoPoliza =(TipoPoliza) Enum.Parse(typeof(TipoPoliza), dto.tipoPoliza),
                vehiculoId = dto.vehiculoId,
            };
        }
        
        public static Poliza MapToEntity( PolizaRegisterDTO dto)
        {
            Poliza poliza = new Poliza();
            poliza.fechaRegistro = dto.fechaRegistro;
            poliza.fechaVencimiento = dto.fechaVencimiento;
            poliza.tipoPoliza =(TipoPoliza) Enum.Parse(typeof(TipoPoliza), dto.tipoPoliza);
            poliza.vehiculoId = dto.vehiculoId;
            return poliza;
        }

        public static PolizaDTO MapToDTO (Poliza entity)
        {
            return new PolizaDTO
            {
                Id = entity.Id,
                fechaRegistro = entity.fechaRegistro,
                fechaVencimiento =  entity.fechaVencimiento,
                tipoPoliza = entity.tipoPoliza.ToString(),
                vehiculoId = entity.vehiculoId
            };
        }


    }
}