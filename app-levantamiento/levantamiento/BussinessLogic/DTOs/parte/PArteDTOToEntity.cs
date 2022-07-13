using levantamiento.BussinesLogic.DTOs;
using levantamiento.Exceptions;
using levantamiento.Persistence.Entities;

namespace levantamiento.BussinesLogic.Logic
{
    public static class ParteDTOToEntity
    {
        public static Parte ConvertParteDTOToEntity(ParteDTO parte)
        {
            try
            {
                return new Parte
                {
                    parteId = parte.Id,
                    nombre = parte.nombre
                };
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al convertir al hacer converison del formatoparte", ex);
            }
            
        }
    }
}