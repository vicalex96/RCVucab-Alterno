using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;

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
        /// registra un asegurado en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="asegurado">DTO de registro con la data de asegurado</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool RegisterAsegurado(AseguradoRegisterDTO asegurado)
        {
            try
            {
                //El nombre y el apellido no pueden estar vacios
                if(asegurado.nombre.ToLower() == "string" || 
                    asegurado.nombre.Count() == 0 || 
                    asegurado.apellido.ToLower() == "string" || 
                    asegurado.apellido.Count() == 0 )
                {
                    throw new RCVInvalidFieldException("El nombre y/o el apellido estan vacios");
                }

                Asegurado aseguradoEntity = new Asegurado{
                    aseguradoId = asegurado.Id, 
                    nombre = asegurado.nombre, 
                    apellido = asegurado.apellido
                };

                return _aseguradoDAO.RegisterAsegurado(aseguradoEntity);
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