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
    public class RequerimientoDAO : IRequerimientoDAO
    {
        public readonly ITallerDBContext _context;

        public RequerimientoDAO(ITallerDBContext context)
        {
            _context = context;
        }

        public List<RequerimientoDTO> GetRequerimientos(Guid solicitudId)
        {
            try
            {
                var data = _context.Requerimientos
                    .Where(r => r.solicitudRepId == solicitudId)
                    .Select(
                        r => new RequerimientoDTO
                        {
                            Id = r.requerimientoId,
                            solicitudRepId = r.solicitudRepId,
                            parteId = r.parteId,
                            descripcion = r.descripcion,
                            tipoRequerimiento = r.tipoRequerimiento.ToString(),
                            cantidad = r.cantidad,
                            estado = r.estado.ToString(),
                        }
                    ).ToList();                
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los requerimientos", ex);
            }
        }
        public bool UpdateTipoRequerimiento(Guid requerimientoId,TipoRequerimiento tipo)
        {
            try
            {
                var requerimiento = _context.Requerimientos.Find(requerimientoId);
                if (requerimiento == null)
                {
                    throw new RCVException("No se encontro el requerimiento");
                }
                requerimiento.tipoRequerimiento = tipo;
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al actualizar el tipo de requerimiento", ex);
            }
        }
        
        public bool UpdateQuantityPiecesRequerimiento(Guid requerimientoId,int quantity)
        {
            try
            {
                var requerimiento = _context.Requerimientos.Find(requerimientoId);
                if (requerimiento == null)
                {
                    throw new RCVException("No se encontro el requerimiento");
                }
                requerimiento.cantidad = quantity;
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al actualizar la cantidad de piezas del requerimiento", ex);
            }
        }

        public bool RegisterRequerimiento(RequerimientoDTO requerimientoDTO)
        {
            try
            {
                if (requerimientoDTO.cantidad < 0)
                    throw new IndexOutOfRangeException();
                if(!Requerimiento.IsTipoRequerimiento(requerimientoDTO.tipoRequerimiento))
                {
                    throw new InvalidDataException();
                }
                
                TipoRequerimiento tipo = Requerimiento.ConvertToTipoRequerimiento(requerimientoDTO.tipoRequerimiento);
                
                var requerimiento = new Requerimiento
                {
                    requerimientoId = Guid.NewGuid(),
                    solicitudRepId = requerimientoDTO.solicitudRepId,
                    parteId = requerimientoDTO.parteId,
                    descripcion = requerimientoDTO.descripcion,
                    tipoRequerimiento = tipo,
                    cantidad = requerimientoDTO.cantidad,
                    estado = EstadoRequerimiento.PorEntregar,
                };
                _context.Requerimientos.Add(requerimiento);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (IndexOutOfRangeException ex)
            {    
                throw new RCVException("La cantidad de piezas del requerimiento no puede ser menor a 0", ex);
            }
            catch(InvalidDataException ex)
            {
                throw new RCVException("El tipo de requerimiento ingresado no es valido", ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el requerimiento", ex);
            }
        }
    }
}
    
