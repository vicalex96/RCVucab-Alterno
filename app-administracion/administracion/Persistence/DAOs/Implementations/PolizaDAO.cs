using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    public class PolizaDAO: IPolizaDAO
    {
        public readonly IAdminDBContext _context;

        public PolizaDAO( IAdminDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// indica si un vehiculo esta disponible para recibir una póliza
        /// </summary>
        /// <param name="vehiculoId">id del vehiculo</param>
        /// <returns>true si esta disponible</returns>
        private bool IsAvailableForPoliza(Guid vehiculoId)
        {
            try
            {
                var countOfActivePolizas = _context.Polizas
                    .Include(p => p.vehiculo)
                    .Include(p => p.vehiculo!.asegurado)
                    .Where(p => p.vehiculoId == vehiculoId 
                        && p.fechaVencimiento > DateTime.Today)
                    .Count();

                if(countOfActivePolizas == 0)
                    return true;

                return false;
            }
            catch(Exception ex)
            {
                throw new RCVException("ocurrio un problema al verificar la disponiblidad para la poliza", ex);
            }
        }

        /// <summary>
        /// reviza que el vehiculo no tiene ningun asegurado asignado
        /// </summary>
        /// <param name="vehiculoId">id del vehiculo</param>
        /// <returns>true si no tiene asegurado asignado</returns>
        private bool IsNotAseguradosVehiculo(Guid vehiculoId)
        {
            try
            {
                var vehiculo = _context.Vehiculos
                    .Where(v => v.vehiculoId == vehiculoId 
                        && v.asegurado == null);

                if(vehiculo != null)
                    return true;

                return false;
            }
            catch(Exception ex)
            {
                throw new RCVException("ocurrio un problema al verificar la disponiblidad para la poliza", ex);
            }
        }

        /// <summary>
        /// registra una poliza nueva, si cumple con las condiciones
        /// </summary>
        /// <param name="poliza">DTO con la informacion de la poliza</param>
        /// <returns>true si se registro correctamente</returns>
        public bool RegisterPoliza (Poliza poliza)
        {
            try
            {
                _context.Polizas.Add(poliza);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(Exception ex){
                throw new RCVException("Error al crear el asegurado", ex);
            }
        }

        /// <summary>
        /// obtiene una poliza por su id
        /// </summary>
        /// <param name="polizaId">id de la poliza</param>
        /// <returns>DTO con la informacion de la poliza</returns>
        public PolizaDTO GetPolizaByGuid(Guid polizaId)
        {
            try
            {
                var poliza = _context.Polizas
                .Where(p => p.polizaId == polizaId)
                .Select( p=> new PolizaDTO{
                    Id = p.polizaId,
                    fechaRegistro = p.fechaRegistro,
                    fechaVencimiento = p.fechaVencimiento,
                    tipoPoliza = p.tipoPoliza.ToString(),
                    vehiculoId = p.vehiculoId
                }).FirstOrDefault();

                return poliza!;

            }
            catch(ArgumentNullException ex)
            {
                throw new RCVException("No se encontraron polizas", ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Error no se obtuvo ninguna poliza", ex);
            }
        }

        /// <summary>
        /// obtiene la poliza activa del vehiculo por el id del vehiculo
        /// </summary>
        /// <param name="vehiculoId">id del vehiculo</param>
        /// <returns>DTO con la informacion de la poliza</returns>
        public PolizaDTO GetPolizaByVehiculoGuid(Guid vehiculoID)
        {
            try
            {
                var poliza = _context.Polizas
                    .Include(p => p.vehiculo)
                    .Include(p => p.vehiculo!.asegurado)
                    .Where(p => p.vehiculoId == vehiculoID 
                        && p.fechaVencimiento > DateTime.Now
                        )
                    .Select( p=> new PolizaDTO{
                        Id = p.polizaId,
                        fechaRegistro = p.fechaRegistro,
                        fechaVencimiento = p.fechaVencimiento,
                        tipoPoliza = p.tipoPoliza.ToString(),
                        vehiculoId = p.vehiculoId,
                        vehiculo = new VehiculoDTO{
                            Id = p.vehiculo!.vehiculoId,
                            anioModelo = p.vehiculo.anioModelo,
                            color = p.vehiculo.color.ToString(),
                            marca = p.vehiculo.marca.ToString(),
                            asegurado =  new AseguradoDTO{
                                Id = p.vehiculo.asegurado!.aseguradoId,
                                nombre = p.vehiculo.asegurado!.nombre,
                                apellido = p.vehiculo.asegurado!.apellido
                            }
                        }
                    }).FirstOrDefault();
                return poliza! ;

            }
            catch(ArgumentNullException ex)
            {
                throw new RCVException("No se encontraron polizas vigentes para el vehiculo solcitado", ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los vehiculos", ex);
            }
        }
    }
}