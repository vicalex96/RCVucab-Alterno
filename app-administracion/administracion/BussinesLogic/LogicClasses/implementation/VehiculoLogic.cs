using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Exceptions;

namespace administracion.BussinesLogic.LogicClasses
{
    public class VehiculoLogic: IVehiculoLogic
    {
        
        private readonly IVehiculoDAO _vehiculoDao;
        private readonly IAseguradoDAO _aseguradoDao;

        public VehiculoLogic (IVehiculoDAO vehiculoDao,IAseguradoDAO aseguradoDao )
        {
            _vehiculoDao = vehiculoDao;
            _aseguradoDao = aseguradoDao;
        }

        /// <summary>
        /// Registra un vehiculo en la base de datos
        /// </summary>
        /// <param name="vehiculoId">Identificador del vehiculo</param>
        /// <param name="aseguradoId">Identificador del asegurado</param>
        /// <returns>True si el vehiculo se registro correctamente, false si no</returns>
        public bool AddAseguradoToVehiculo(Guid vehiculoId, Guid aseguradoId)
        {
            try
            {
                VehiculoDTO vehiculoDTO = _vehiculoDao.GetVehiculoByGuid(vehiculoId);

                if(vehiculoDTO == null)
                    throw new Exception("El vehiculo no existe en el sistema");
                if(vehiculoDTO.asegurado != null)
                    throw new Exception("El vehiculo ya esta asignado a un asegurado");

                _vehiculoDao.AddAsegurado(vehiculoId, aseguradoId);

                return true;
            }
            catch(RCVException ex)
            {
                throw new RCVException(ex.Mensaje,ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un error desconocido",ex);
            }
        }
    }
}