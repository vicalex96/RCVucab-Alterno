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


        
        public bool RegisterIncidente (IncidenteSimpleDTO incidente)
        {
            try
            {
                
                Incidente incidenteEntity = new Incidente{
                    incidenteId = incidente.incidenteId,
                    polizaId = incidente.polizaId,
                    estadoIncidente = EstadoIncidente.Pendiente,
                    fechaRegistrado = DateTime.Today,
                };   
                _context.Incidentes.Add(incidenteEntity);
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
        public bool actualizarIncidente(Guid incidenteId, EstadoIncidente estado)
        {
            try
            {
                var incidente = _context.Incidentes
                    .Where(i => i.incidenteId == incidenteId)
                    .FirstOrDefault();
                if(incidente != null)
                {
                    incidente.estadoIncidente = estado;
                    _context.DbContext.SaveChanges();
                    return true;
                }
                throw new RCVException("");
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