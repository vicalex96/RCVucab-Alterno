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

        ///<summary>
        ///actualiza la lista de incidentes desde el sistema de administracion
        ///</summary>
        ///<param name="incidente">incidente a registrar</param>
        ///<returns>true si se registro correctamente, false si no</returns>
        public bool RegisterIncidente(Incidente incidente)
        {
            try
            {
                _context.Incidentes.Add(incidente);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///<summary>
        ///Muestra todos los incidentes
        ///</summary>
        ///<returns>lista de incidentes</returns>
        public ICollection<IncidenteToShowDTO> GetAll()
        {
            try
            {
                return _context.Incidentes
                .Include(s => s.solicitudes)
                .Select(i => new IncidenteToShowDTO
                {
                    Id = i.incidenteId,
                    solicitudesRepacion = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Where(r => r.incidenteId == i.incidenteId)
                    .Select(s => new SolicitudesReparacionDTO
                    {
                        Id = s.SolicitudReparacionId,
                        incidenteId = s.incidenteId,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///<summary>
        ///Muestra los incidentes que no tengan solicitudes asociadas
        ///</summary>
        ///<returns>lista de incidentes</returns>
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud()
        {
            try
            {
                return (from nc in _context.Incidentes
                    join s in _context.SolicitudesReparacion on nc.incidenteId equals s.incidenteId into relacion
                    from s in relacion.DefaultIfEmpty()
                    where s == null
                    select new IncidenteToShowDTO
                    {
                        Id = nc.incidenteId

                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///<summary>
        ///Realiza una busqueda por id del incidente y devuelve el incidente
        ///</summary>
        ///<param name="incidenteId">id del incidente</param>
        ///<returns>incidente</returns>
        public IncidenteDTO GetIncidenteById(Guid incidenteId)
        {
            try
            {
                return _context.Incidentes
                .Include(s => s.solicitudes)
                .Where(i => i.incidenteId == incidenteId)
                .Select(i => new IncidenteDTO
                {
                    Id = i.incidenteId,
                    solicitudes = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Select(s => new SolicitudesReparacionDTO
                    {
                        Id = s.SolicitudReparacionId,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }
    }
}