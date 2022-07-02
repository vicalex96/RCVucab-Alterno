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
    public class ParteDAO : IParteDAO
    {
        public readonly ITallerDBContext _context;

        public ParteDAO(ITallerDBContext context)
        {
            _context = context;
        }
        public List<ParteDTO> GetPartes()
        {
            try
            {
                var data = _context.partes
                .Select(p => new ParteDTO
                {
                    Id = p.parteId,
                    nombre = p.nombre,
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los partes", ex);
            }
        }
        public bool RegisterParte(ParteDTO parte)
        {
            try
            {
                var newParte = new Parte
                {
                    parteId = parte.Id,
                    nombre = parte.nombre,
                };
                _context.partes.Add(newParte);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el parte", ex);
            }
        }
    }
} 
