using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.Mappers;

namespace administracion.BussinesLogic.LogicClasses
{
    public class AseguradoLogic: IAseguradoLogic
    {
        private readonly IAseguradoDAO _aseguradoDAO;

        public AseguradoLogic (IAseguradoDAO aseguradoDao)
        {
            _aseguradoDAO = aseguradoDao;
        }

        /// <summary>
        /// verifica que si el nombre o apellido estan vacios
        /// </summary>
        /// <param name="name">campo nombre a revisar</param>
        /// <returns>booleano true si es invalido, false si es valido</returns>
        private bool IsNotValidName( string name)
        {
            if(name.ToLower() == "string" || name.Count() == 0)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// registra un asegurado en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="asegurado">DTO de registro con la data de asegurado</param>
        /// <returns>boleano true si todo salio bien</returns>
        public int RegisterAsegurado(AseguradoRegisterDTO asegurado)
        {
            try
            {
                //El nombre y el apellido no pueden estar vacios
                if(IsNotValidName(asegurado.nombre) || IsNotValidName(asegurado.apellido) )
                {
                    throw new RCVInvalidFieldException("El campo nombre no es valido, debe ser diferente de vacio y no por defecto");
                }
                if(IsNotValidName(asegurado.apellido))
                {
                    throw new RCVInvalidFieldException("El campo apellido no es valido, debe ser diferente de vacio y no por defecto");
                }
            
                return _aseguradoDAO.RegisterAsegurado(
                            AseguradoMapper.MapToEntity(asegurado)
                        );
            }
            catch(RCVInvalidFieldException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al registrar asegurado", ex);
            }

        }
    }

}