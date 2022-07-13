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

        ///<summary>
        ///Muestra todas los requerimientos asociados a una solicitud
        ///</summary>
        ///<param name="solicitudId">id de la solicitud</param>
        ///<returns>lista de requerimientos</returns>
        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId)
        {
            try
            {
                return _context.Requerimientos
                .Where(x => x.solicitudReparacionId == solicitudId)
                .Select(r => new RequerimientoDTO{
                    Id = r.requerimientoId,
                    solicitudId = r.solicitudReparacionId,
                    parteId = r.parteId, 
                    descripcion = r.descripcion,
                    tipoRequerimiento = r.tipoRequerimiento.ToString(),
                    cantidad = r.cantidad,
                    parte = new ParteDTO{
                        Id = r.parte!.parteId,
                        nombre = r.parte.nombre,
                    }
                })
                .ToList();
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los requerimientos, no se encontraron", ex);
            }
        }

        ///<summary>
        ///Registra un nuevo requerimiento en el sistema, asegurandose que cumpla con las restricciones necesarias
        ///</summary>
        ///<param name="requerimiento">requerimiento a registrar</param>
        ///<returns>true si se registro correctamente</returns>
        public bool RegisterRequerimiento(Requerimiento requerimiento)
        {
            try
            {
                _context.Requerimientos.Add(requerimiento);
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