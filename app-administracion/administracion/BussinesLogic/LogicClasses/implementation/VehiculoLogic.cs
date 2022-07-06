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

        public VehiculoLogic (IVehiculoDAO vehiculoDao,IAseguradoDAO aseguradoDao)
        {
            _vehiculoDao = vehiculoDao;
            _aseguradoDao = aseguradoDao;
        }

        /// <summary>
        /// registra un Vehiculo en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="Vehiculo">DTO de registro con la data de Vehiculo</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool RegisterVehiculo(VehiculoRegisterDTO vehiculo)
        {
            try
            {
                //convierte el string de color y marca a enum, si no lo logra retorna una ArgumentException
                Color _color = (Color)Enum
                    .Parse(typeof(Color), vehiculo.color);
                Marca _marca = (Marca)Enum
                    .Parse(typeof(Marca), vehiculo.marca);
                if(vehiculo.placa.Count() >7)
                    throw new ArgumentException();

                var vehiculoEntity = new Vehiculo();
                vehiculoEntity.vehiculoId = vehiculo.Id;
                vehiculoEntity.anioModelo = vehiculo.anioModelo;
                vehiculoEntity.fechaCompra = vehiculo.fechaCompra;
                vehiculoEntity.color = _color;
                vehiculoEntity.placa = vehiculo.placa;
                vehiculoEntity.marca = _marca;

                return _vehiculoDao.RegisterVehiculo(vehiculoEntity);
            }
            catch(ArgumentException ex)
            {
                throw new RCVInvalidFieldException("Error: alguno de los argumentos no es valido, color y marca deben de existir en el sistema, la placa tiene un maximo de 7 caracteres", ex);
            }
            catch (Exception e)
            {
                throw new RCVException("Error al registrar el incidente", e);
            }
        }

        /// <summary>
        /// Registra un vehiculo en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="vehiculoId">Identificador del vehiculo</param>
        /// <param name="aseguradoId">Identificador del asegurado</param>
        /// <returns>True si el vehiculo se registro correctamente, false si no</returns>
        public bool AddAseguradoToVehiculo(Guid vehiculoId, Guid aseguradoId)
        {
            try
            {
                //El vehiculo tiene que existir
                VehiculoDTO vehiculoDTO = _vehiculoDao.GetVehiculoByGuid(vehiculoId);
                if(vehiculoDTO == null)
                    throw new RCVNullException("El vehiculo no existe en el sistema");
                
                //El vehiculo no puede tener un asegurado ya registrado
                if(vehiculoDTO.asegurado != null)
                    throw new RCVAsociationException("El vehiculo ya esta asignado a un asegurado");

                //el asegurado tiene que estar registrado
                if(_aseguradoDao.GetAseguradoByGuid(aseguradoId) == null)
                    throw new RCVNullException("No existe ningun asegurado con dicho Id");

                _vehiculoDao.AddAsegurado(vehiculoId, aseguradoId);
                return true;
            }
            catch(RCVNullException ex)
            {
                throw ex;
            }
            catch(RCVAsociationException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un error desconocido",ex);
            }
        }
    
    }
}