using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;

namespace levantamiento.BussinesLogic.DTOs
{
    public static class RequerimientoDTOToEntity
    {


        public static Requerimiento ConvertDTOToEntity(RequerimientoRegisterDTO requerimiento)
        {
            try
            {
                return new Requerimiento
                {
                    requerimientoId = requerimiento.Id,
                    solicitudReparacionId = requerimiento.solicitudId,
                    parteId = requerimiento.parteId,
                    descripcion = requerimiento.descripcion,
                    tipoRequerimiento =  (TipoRequerimiento)Enum.Parse(typeof(TipoRequerimiento), requerimiento.tipoRequerimiento),
                    cantidad = requerimiento.cantidad,
                };
            }
            catch(ArgumentException ex)
            {
                throw new RCVInvalidFieldException("El tipo de requerimiento es invalido", ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio algun problema a la hora de hacer el registro", ex);
            }
        }
    }
}