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
    public class SolicitudDAO : ISolicitudDAO
    {
        public readonly ITallerDBContext _context;

        public SolicitudDAO(ITallerDBContext context)
        {
            _context = context;
        }
        public bool UpdateList(ICollection<SolicitudQueueDTO> dataList)
        {
            try
            {
                foreach (SolicitudQueueDTO elemento in dataList)
                {
                    SolicitudReparacion incidente = new SolicitudReparacion
                    {
                        solicitudRepId = elemento.Id
                    };
                    _context.SolicitudReparacions.Add(incidente);
                    _context.DbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al actualizar el listado de incidentes", ex);
            }
            return true;
        }
        public List<SolicitudDTO> GetSolicitudes()
        {
            List<SolicitudDTO> solicitudes = new List<SolicitudDTO>();
            try
            {
                solicitudes = _context.SolicitudReparacions
                .Select(s => new SolicitudDTO
                {
                    solicitudRepId = s.solicitudRepId,
                    incidenteId = s.incidenteId,
                    vehiculoId = s.vehiculoId,
                    tallerId = s.tallerId,
                }
                )
                .ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener las solicitudes", ex);
            }
            return solicitudes;
        }
    }
} 
