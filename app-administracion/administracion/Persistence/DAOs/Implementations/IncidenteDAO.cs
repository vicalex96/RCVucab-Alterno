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
        public IncidenteDTO GetIncidenteById(Guid incidenteID)
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
        /// Obtiene una lista de incidentes que se encuentren en el estado solicitado
        /// </summary>
        /// <returns>Lista de incidentes</returns>
        public List<IncidenteDTO> GetIncidentesByState(EstadoIncidente estado)
        {
            try
            {
                var incidentes =  _context.Incidentes
                .Include(i => i.poliza)
                .Where( i => i.estadoIncidente == estado)
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
        public bool UpdateIncidente(Incidente incidente)
        {
            try
            {
                _context.DbContext.Update(incidente);
                _context.DbContext.SaveChanges();
                return true;

            }

            catch (Exception ex)
            {
                throw new RCVUpdateException("Acurrio un problema y no se pudo actualizar el incidente", ex);
            }
        }

    }


}