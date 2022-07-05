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
    
    public class RequerimientoDAO : IRequerimientoDAO
    {
        public readonly ILevantamientoDBContext _context;

        public RequerimientoDAO(ILevantamientoDBContext context)
        {
            _context = context;
        }

        ///Muestra todas los requerimientos asociados a una solicitud
        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId)
        {
            try
            {
                var data = _context.Requerimientos
                .Where(x => x.solicitudReparacionId == solicitudId)
                .Select(r => new RequerimientoDTO{
                    Id = r.requerimientoId,
                    solicitudId = r.solicitudReparacionId,
                    parteId = r.parteId, 
                    descripcion = r.descripcion,
                    tipoRequerimiento = r.tipoRequerimiento.ToString(),
                    cantidad = r.cantidad,
                    parte = new ParteDTO{
                        Id = r.parte.parteId,
                        nombre = r.parte.nombre,
                    }
                })
                .ToList();
                if(data.Count == 0){
                    throw new Exception();
                }
                return data;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los requerimientos, no se encontraron", ex);
            }
        }

        ///Registra un nuevo requerimiento en el sistema, asegurandose que cumpla con las restricciones necesarias
        public bool RegisterRequerimiento(RequerimientoRegisterDTO requerimiento)
        {
            try
            {
                var solicitud = _context.SolicitudesReparacion.Where(s => s.SolicitudReparacionId == requerimiento.solicitudId).FirstOrDefault();
                var parte = _context.Partes.Where(p => p.parteId == requerimiento.parteId).FirstOrDefault();

                if(solicitud == null)
                    throw new RCVException("Error: no se puede registra el requerimiento, la solicitud no existe");

                if(parte == null)
                    throw new RCVException("Error: no se puede registra el requerimiento, el parte no existe");

                if(requerimiento.cantidad <= 0)
                    throw new ArgumentException();

                var requerimientoEntity = new Requerimiento
                {
                    requerimientoId = requerimiento.Id,
                    solicitudReparacionId = requerimiento.solicitudId,
                    parteId = requerimiento.parteId,
                    descripcion = requerimiento.descripcion,
                    tipoRequerimiento =  (TipoRequerimiento)Enum.Parse(typeof(TipoRequerimiento), requerimiento.tipoRequerimiento),
                    cantidad = requerimiento.cantidad,
                };
                _context.Requerimientos.Add(requerimientoEntity);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(ArgumentException ex)
            {
                throw new RCVException("Error: puede ser que el tipo de requerimeinto no sea valido o la cantidad de piesas a reparar sea invalida", ex);
            }
            catch(RCVException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error: No se logro registrar el requerimiento por algun error desconocido", ex);
            }
        }
    }
}