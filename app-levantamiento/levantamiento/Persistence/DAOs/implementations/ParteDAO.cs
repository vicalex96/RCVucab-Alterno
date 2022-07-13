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

        ///<summary>
        ///Muestra todas las partes registradas en el sistema
        ///</summary>
        ///<returns>lista de partes</returns>
        public List<ParteDTO> GetAll()
        {
            try
            {
                return _context.Partes
                .Select(p => new ParteDTO
                {
                    Id = p.parteId,
                    nombre = p.nombre
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la lista de partes", ex);
            }
        }

        ///<summary>
        ///Obtener la informacion de una parte por su Id
        ///</summary>
        //<param name="parteId">identificador de la piesa</param>
        ///<returns>devuelve la informacion de la piesa o vacio</returns>
        public ParteDTO GetParteById(Guid parteId)
        {
            try
            {
                return _context.Partes
                .Where(p => p.parteId == parteId)
                .Select(p => new ParteDTO
                {
                    Id = p.parteId,
                    nombre = p.nombre
                }).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener la lista de partes", ex);
            }
        }
        ///<summary>
        ///Registra una nueva parte en el sistema
        ///</summary>
        ///<param name="parte">parte a registrar</param>
        ///<returns>true si se registro correctamente</returns>
        public bool RegisterParte(Parte parte)
        {
            try
            {
                _context.Partes.Add(parte);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar la parte", ex);
            }
            
        }
    }
}