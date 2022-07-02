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
    public class ParteDAO :IParteDAO
    {
        public readonly ILevantamientoDBContext _context;

        public ParteDAO(ILevantamientoDBContext context)
        {
            _context = context;
        }

        ///Muestra todas las partes registradas en el sistema
        public List<ParteDTO> GetAll()
        {
            try
            {
                var data = _context.Partes
                .Select(p => new ParteDTO
                {
                    Id = p.parteId,
                    nombre = p.nombre
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la lista de partes", ex);
            }
        }

        ///Registra una nueva parte en el sistema
        public bool RegisterParte(ParteDTO Parte)
        {
            try
            {
                Parte parte = new Parte
                {
                    parteId = Parte.Id,
                    nombre = Parte.nombre
                };
                _context.Partes.Add(parte);
                _context.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar la parte", ex);
            }
            return true;
        }
    }
}