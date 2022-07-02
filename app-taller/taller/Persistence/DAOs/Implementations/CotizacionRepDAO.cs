using Microsoft.EntityFrameworkCore;
using taller.Persistence.Database;
using taller.Persistence.Entities;
using taller.Exceptions;
using taller.BussinesLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace taller.Persistence.DAOs
{
    public class CotizacionRepDAO : ICotizacionRepDAO
    {
        public readonly ITallerDBContext _context;

        public CotizacionRepDAO(ITallerDBContext context)
        {
            _context = context;
        }

        public List<CotizacionRepDTO> GetCotizaciones()
        {
            try
            {
                var data =  _context.CotizacionReparaciones
                    .Select(c => new CotizacionRepDTO
                    {
                        Id = c.cotizacionRepId,
                        tallerId = c.tallerId,
                        solicitudRepId = c.solicitudRepId,
                        estado = c.estado.ToString(),
                        costoManoObra = c.costoManoObra,
                        fechaInicioReparacion = c.fechaInicioReparacion,
                        fechaFinReparacion = c.fechaFinReparacion,
                    }
                    )
                    .ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener las cotizaciones", ex);
            }
        }
        public CotizacionRepDTO GetCotizacionRep(Guid SolicutdId)
        {
            try
            {
                var data = _context.CotizacionReparaciones
                    .Where(r => r.solicitudRepId == SolicutdId)
                    .Select(
                        r => new CotizacionRepDTO
                        {
                            Id = r.cotizacionRepId,
                            solicitudRepId = r.solicitudRepId,
                            tallerId = r.tallerId,
                            fechaInicioReparacion = r.fechaInicioReparacion,
                            fechaFinReparacion = r.fechaFinReparacion,
                            estado = r.estado.ToString(),
                            costoManoObra = r.costoManoObra,
                            
                        }
                    ).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la cotizacion de reparacion", ex);
            }
        }
        public bool RegisterCotizacionReparacion(CotizacionRepSimpleDTO cotizacionRep)
        {
            try
            {
                if(GetCotizacionRep(cotizacionRep.solicitudRepId) != null) 
                    throw new Exception();
                if(cotizacionRep.costoManoObra < 0)
                    throw new ArgumentOutOfRangeException();
                
                if( cotizacionRep.Id == null ||
                    cotizacionRep.solicitudRepId == null ||
                    cotizacionRep.tallerId == null)
                        throw new ArgumentNullException();
                        
                CotizacionReparacion cotizacion = new CotizacionReparacion
                {
                    cotizacionRepId = cotizacionRep.Id,
                    solicitudRepId = cotizacionRep.solicitudRepId,
                    tallerId = cotizacionRep.tallerId,
                    estado = EstadoCotRep.Pendiente,
                    costoManoObra = cotizacionRep.costoManoObra,
                };

                var data = _context.CotizacionReparaciones.Add(cotizacion);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(ArgumentNullException ex)
            {
                throw new RCVException("Los identificadores ID no pueden estar vacios", ex);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                throw new RCVException("El costo de la mano de obra no puede ser 0", ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar la cotizacion de reparacion", ex);
            }
        }
        public bool UpdateEstadoCotizacion(Guid cotizacionId, EstadoCotRep estado)
        {
            try
            {
                    
                var data = _context.CotizacionReparaciones
                    .Where(r => r.cotizacionRepId == cotizacionId 
                        && r.estado == EstadoCotRep.Pendiente)
                    .Single();
                if(data == null)
                    throw new Exception();
                data.estado = estado;
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("La cotizacion a la que se le desea modificar el estado no existe o no esta disponible", ex);
            }
        }

        public bool UpdateFechaInicioReparacion(Guid cotizacionRepId,DateTime fechaInicio)
        {
            try
            {
                var data = _context.CotizacionReparaciones
                    .Where(r => r.cotizacionRepId == cotizacionRepId 
                        && (r.estado == EstadoCotRep.OrdenDeCompra
                        || r.estado == EstadoCotRep.Facturado)
                        && r.fechaInicioReparacion == null)
                    .Single();
                if(data == null)
                    throw new Exception();
                data.fechaInicioReparacion = fechaInicio;
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("La cotizacion a la que se le desea modificar la fecha de inicio de reparacion no existe o no esta disponible, no pueden tener fecha aun, estar en facturado o con orden de compra", ex);
            }
        }
        public bool UpdateFechaFinReparacion(Guid cotizacionRepId,DateTime fechaFin)
        {
            try
            {
                var data = _context.CotizacionReparaciones
                    .Where(r => r.cotizacionRepId == cotizacionRepId 
                        && (r.estado == EstadoCotRep.OrdenDeCompra
                        || r.estado == EstadoCotRep.Facturado)
                        && r.fechaFinReparacion == null)
                    .Single();
                if(data == null)
                    throw new Exception();
                data.fechaFinReparacion = fechaFin;
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("La cotizacion a la que se le desea modificar la fecha de fin de reparacion no existe o no esta disponible", ex);
            }
        }

    }
}
    
