using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.Mappers;

namespace levantamiento.BussinesLogic.Logic
{
    public class ParteLogic: IParteLogic
    {

        private readonly IParteDAO _parteDAO;

        public ParteLogic( IParteDAO parteDAO)
        {
            _parteDAO = parteDAO;
        }
        
        /// <summary>
        /// Verifica si un nombre no es valido
        /// </summary>
        /// <param name="nombre">Nombre a verificar</param>
        /// <returns>True si no es valido, false si lo es</returns>
        private bool IsNotValidName(string nombre)
        {
            if(nombre == "string" || nombre == "")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ejecuta la logica para registrar una parte en el sistema
        /// </summary>
        /// <param name="parte">Parte a registrar</param>
        /// <returns>True si se registro correctamente</returns>
        public bool RegisterParte(ParteDTO parteDTO)
        {
            try
            {
                if(IsNotValidName(parteDTO.nombre))
                {
                    throw new RCVInvalidFieldException("El nombre de la parte no puede ser vacio");
                }
                _parteDAO.RegisterParte(
                    ParteMapper.MapToEntity(parteDTO)
                );
                return true;
            }
            catch (RCVInvalidFieldException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RCVException("no se logró registrar la pieza", ex);
            }
        }

    }
}