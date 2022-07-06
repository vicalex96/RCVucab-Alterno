using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;


namespace administracion.Persistence.DAOs
{
    public class IncidenteDAO: IIncidenteDAO
    {
        public readonly IAdminDBContext _context;

        public IncidenteDAO( IAdminDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un incidente nuevo
        /// </summary>
        /// <param name="incidente">DTO de registro con la informacion del incidente</param>
        /// <returns>booleano true</returns>
        public bool RegisterIncidente (Incidente incidente)
        {
            try
            {
                
                _context.Incidentes.Add(incidente);
                _context.DbContext.SaveChanges();

                return true;
            }
            catch(DbUpdateException ex)
            {
                throw new RCVException("No se pudo registrar, errar con al identificar la relacion con las claves",ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("ocurrio un errror", ex);
            }    
        }
        
        /// <summary>
        /// Obtiene un incidente según el Id del incidente
        /// </summary>
        /// <param name="incidenteId">Id del incidente</param>
        /// <returns>DTO con la informacion del incidente</returns>
        public IncidenteDTO consultarIncidente(Guid incidenteID)
        {
            try
            {
                var incidente =  _context.Incidentes
                .Include(i => i.poliza)
                .Where( i => i.incidenteId == incidenteID)
                .Select(i => new IncidenteDTO{
                    Id = i.incidenteId,
                    polizaId = i.polizaId,
                    estadoIncidente = i.estadoIncidente.ToString(),
                    poliza = new PolizaDTO{
                        Id = i.poliza!.polizaId,
                        fechaRegistro = i.poliza.fechaRegistro,
                        fechaVencimiento = i.poliza.fechaVencimiento,
                        tipoPoliza = i.poliza.tipoPoliza.ToString(),
                        vehiculoId = i.poliza.vehiculoId,   
                    }!
                }).FirstOrDefault();

                return incidente!;   
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los vehiculos", ex);
            }
        }
        
        /// <summary>
        /// Obtiene una lista de incidentes que estan actualimente activos
        /// </summary>
        /// <returns>Lista de incidentes</returns>
        public List<IncidenteDTO> ConsultarIncidentesActivos()
        {
            try
            {
                var incidentes =  _context.Incidentes
                .Include(i => i.poliza)
                .Where( i => i.estadoIncidente != EstadoIncidente.cerrado)
                .Select(i => new IncidenteDTO{
                    Id = i.incidenteId,
                    polizaId = i.polizaId,
                    estadoIncidente = i.estadoIncidente.ToString(),
                    poliza = new PolizaDTO{
                        Id = i.poliza!.polizaId,
                        fechaRegistro = i.poliza.fechaRegistro,
                        fechaVencimiento = i.poliza.fechaVencimiento,
                        tipoPoliza = i.poliza.tipoPoliza.ToString(),
                        vehiculoId = i.poliza.vehiculoId,   
                    }!
                }).ToList();

                return incidentes;   

            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los vehiculos", ex);
            }
        }     
        
        /// <summary>
        /// Actualiza el estado del incidente
        /// </summary>
        /// <param name="incidenteId">Id del incidente</param>
        /// <param name="EstadoIncidente">Estado del incidente</param>
        /// <returns>booleano true</returns>
        public bool actualizarIncidente(Incidente incidente)
        {
            try
            {
                _context.DbContext.Update(incidente);
                _context.DbContext.SaveChanges();
                return true;

            }
            catch (RCVException ex)
            {
                throw new RCVException("No se encontro ningun incidente con dicho identificador", ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("No se pudo actualizar el incidente", ex);
            }
        }

    }


}