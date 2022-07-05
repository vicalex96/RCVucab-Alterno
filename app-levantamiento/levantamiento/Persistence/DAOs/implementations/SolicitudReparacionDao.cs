using Microsoft.EntityFrameworkCore;
using levantamiento.Persistence.Database;
using levantamiento.Persistence.Entities;
using levantamiento.Exceptions;
using levantamiento.BussinesLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using levantamiento.Conections.APIs;
using levantamiento.Conections.rabbit;

namespace levantamiento.Persistence.DAOs
{
    public class SolcitudReparacionDAO : ISolicitudReparacionDAO
    {
        public readonly ILevantamientoDBContext _context;

        public SolcitudReparacionDAO(ILevantamientoDBContext context)
        {
            _context = context;
        }
        
        //muestra todas las solicitudes en el sistema
        public List<SolicitudesResparacionDTO> GetAll()
        {
            try
            {
                var data = _context.SolicitudesReparacion
                .Include(s => s.requerimientos)
                .Select(s => new SolicitudesResparacionDTO
                {
                    Id = s.SolicitudReparacionId,
                    incidenteId = s.incidenteId,
                    incidente = new IncidenteDTO
                    {
                        Id = s.incidenteId,
                        polizaId = s.incidente.polizaId,
                    },
                    vehiculoId = s.vehiculoId,
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la solicitud", ex);
            }
        }

        //Muestra todas las solicitudes que les falte asignar el taller
        public List<SolicitudesResparacionDTO> GetSolicitudWithoutTaller()
        {
            try
            {
                var data = _context.SolicitudesReparacion
                .Include(s => s.requerimientos)
                .Where(s => s.tallerId == Guid.Empty)
                .Select(s => new SolicitudesResparacionDTO
                {
                    Id = s.SolicitudReparacionId,
                    incidenteId = s.incidenteId,
                    incidente = new IncidenteDTO
                    {
                        Id = s.incidenteId,
                        polizaId = s.incidente.polizaId,
                    },
                    vehiculoId = s.vehiculoId,
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la solicitud", ex);
            }
        }
        
        /// Busca una solicitud por su id 
        public SolicitudesResparacionDTO GetSolicitudById(Guid solicitudId)
        {
            try
            {
                var data = _context.SolicitudesReparacion
                .Include(s => s.requerimientos)
                .Where(s => s.SolicitudReparacionId == solicitudId)
                .Select(s => new SolicitudesResparacionDTO
                {
                    Id = s.SolicitudReparacionId,
                    incidenteId = s.incidenteId,
                    vehiculoId = s.vehiculoId,
                    tallerId = s.tallerId,
                    requerimientos = _context.Requerimientos
                    .Where(r => r.solicitudReparacionId == s.SolicitudReparacionId)
                    .Select(r => new RequerimientoDTO
                    {
                        Id = r.requerimientoId,
                        solicitudId = r.solicitudReparacionId,
                        descripcion = r.descripcion,
                        tipoRequerimiento = r.tipoRequerimiento.ToString(),
                        cantidad = r.cantidad,
                        parte = new ParteDTO{
                            Id = r.parteId,
                            nombre = r.parte.nombre,
                        }
                    }).ToList()
                }).FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la solicitud", ex);
            }
        }

        /// Busca las solicitudes segun el id del incidente
        public List<SolicitudesResparacionDTO> GetSolicitudByIncidenteId(Guid incidenteId)
        {
            try
            {
                var data = _context.SolicitudesReparacion
                .Include(s => s.requerimientos)
                .Where(s => s.incidenteId == incidenteId)
                .Select(s => new SolicitudesResparacionDTO
                {
                    Id = s.SolicitudReparacionId,
                    incidenteId = s.incidenteId,
                    vehiculoId = s.vehiculoId,
                    tallerId = s.tallerId,
                    requerimientos = _context.Requerimientos
                    .Where(r => r.solicitudReparacionId == s.SolicitudReparacionId)
                    .Select(r => new RequerimientoDTO
                    {
                        Id = r.requerimientoId,
                        solicitudId = r.solicitudReparacionId,
                        descripcion = r.descripcion,
                        tipoRequerimiento = r.tipoRequerimiento.ToString(),
                        cantidad = r.cantidad,
                        parte = new ParteDTO{
                            Id = r.parteId,
                            nombre = r.parte.nombre,
                        }
                    }).ToList()
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la solicitud", ex);
            }
        }


        /// Registra una solicitud con los datos basicos, aun no incluye taller ni los requerimientos
        public bool RegisterSolicitud(SolicitudesRespacionRegisterDTO solicitudDTO)
        {
            try
            {
                SolicitudReparacion solicitud = new SolicitudReparacion
                {
                    SolicitudReparacionId = solicitudDTO.Id,
                    incidenteId = solicitudDTO.incidenteId,
                    vehiculoId = solicitudDTO.vehiculoId,
                    fechaSolicitud = DateTime.Today,
                };
                var data = _context.SolicitudesReparacion.Add(solicitud);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar la solicitud", ex);
            }
        }

        public bool AddTaller(Guid solicitudId, Guid tallerId)
        {
            try
            {
                var requerimeintos = _context.Requerimientos
                .Where(r => r.solicitudReparacionId == solicitudId)
                .ToList();
                if(requerimeintos.Count() == 0)
                {
                    throw new RCVException("No se puede asignar un taller a una solicitud sin requerimientos");
                }

                var solicitud = _context.SolicitudesReparacion
                .Where(s => s.SolicitudReparacionId == solicitudId)
                .FirstOrDefault();
                if(solicitud.tallerId == Guid.Empty)
                {
                    solicitud.tallerId = tallerId;
                    _context.DbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new RCVException("Error: la solicitud ya tien un taller asignado");
                }
            }
            catch (RCVException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error: ocurrio un problema al intentar registrar el taller", ex);
            }
        }
    
        public bool SendNotificationsToQueue()
        {
            try
            {
                var data = _context.SolicitudesReparacion
                .Where(s => s.tallerId != Guid.Empty)
                .ToList();
                ProductorRabbit productor = new ProductorRabbit(
                        Exchanges.levantamiento,
                        Routings.taller
                    );
                foreach (var solicitud in data)
                {
                    
                    productor.SendMessage("registrar_solicitud", solicitud.SolicitudReparacionId.ToString());
                }
                productor.SendMessage("registrar_solicitud", "0c5c3262-d5ef-46c7-bc0e-97530821c0cc");
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al enviar las notificaciones a la cola", ex);
            }
        }
    }
}