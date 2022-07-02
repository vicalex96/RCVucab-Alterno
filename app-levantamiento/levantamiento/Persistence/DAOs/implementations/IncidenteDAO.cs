using Microsoft.EntityFrameworkCore;
using levantamiento.Persistence.Database;
using levantamiento.Persistence.Entities;
using levantamiento.Exceptions;
using levantamiento.BussinesLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using levantamiento.Conections.APIs;

namespace levantamiento.Persistence.DAOs
{
    public class IncidenteDAO : IIncidenteDAO
    {
        public readonly ILevantamientoDBContext _context;

        public IncidenteDAO(ILevantamientoDBContext context)
        {
            _context = context;
        }

        ///actualiza la lista de incidentes desde el sistema de administracion
        public bool UpdateList(ICollection<IncidenteQueueDTO> dataList)
        {
            try
            {
                foreach (IncidenteQueueDTO elemento in dataList)
                {
                    Incidente incidente = new Incidente
                    {
                        incidenteId = elemento.Id
                    };
                    _context.Incidentes.Add(incidente);
                    _context.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al actualizar el listado de incidentes", ex);
            }
            return true;
        }

        ///Muestra todos los incidentes
        public ICollection<IncidenteToShowDTO> GetAll()
        {
            try
            {
                var data = _context.Incidentes
                .Include(s => s.solicitudes)
                .Select(i => new IncidenteToShowDTO
                {
                    Id = i.incidenteId,
                    solicitudesRespacion = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Where(r => r.incidenteId == i.incidenteId)
                    .Select(s => new SolicitudesResparacionDTO
                    {
                        Id = s.SolicitudReparacionId,
                        incidenteId = s.incidenteId,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///Muestra los incidentes que no tengan solicitudes asociadas
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud()
        {
            try
            {
                var data = from nc in _context.Incidentes
                    join s in _context.SolicitudesReparacion on nc.incidenteId equals s.incidenteId into relacion
                    from s in relacion.DefaultIfEmpty()
                    where s == null
                    select new IncidenteToShowDTO
                    {
                        Id = nc.incidenteId

                    };
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///Realiza una busqueda por id del incidente y devuelve el incidente
        public ICollection<IncidenteDTO> GetIncidenteById(Guid incidenteId)
        {
            try
            {
                var data = _context.Incidentes
                .Include(s => s.solicitudes)
                .Where(i => i.incidenteId == incidenteId)
                .Select(i => new IncidenteDTO
                {
                    Id = i.incidenteId,
                    solicitudes = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Select(s => new SolicitudesResparacionDTO
                    {
                        Id = s.SolicitudReparacionId,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///Realiza una busqueda detallada de los datos del incidente
        //incluye data de la base de datos de administracion
        public async Task<IncidenteDTO> GetDetailedIncidente (Guid IncidenteId)
        {
            try
            {
                IncidenteAPI incidenteAPI = new IncidenteAPI();
                IncidenteDTO incidente = await incidenteAPI.GetIncidenteFromAdmin(IncidenteId);
                incidente.solicitudes = _context.SolicitudesReparacion.Where(x => x.incidenteId == IncidenteId).Select(s => new SolicitudesResparacionDTO{
                    Id = s.SolicitudReparacionId,
                    incidenteId = s.incidenteId,
                    vehiculoId = s.vehiculoId,
                    tallerId = s.tallerId
                }).ToList();
                return incidente;
            }
            catch(RCVException ex)
            {
                throw new RCVException(ex.Mensaje, ex);
            }
        }
    }
}