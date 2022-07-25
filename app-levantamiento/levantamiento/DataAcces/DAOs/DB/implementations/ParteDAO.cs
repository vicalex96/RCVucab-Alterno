using Microsoft.EntityFrameworkCore;
using levantamiento.DataAccess.Database;
using levantamiento.DataAccess.Entities;
using levantamiento.Exceptions;
using levantamiento.BussinesLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using levantamiento.Conections.APIs;

namespace levantamiento.DataAccess.DAOs
{
    public class ParteDAO :IParteDAO
    {
        private static DesignTimeDbContextFactory desing = new DesignTimeDbContextFactory();

        private ILevantamientoDBContext _context = desing.CreateDbContext(null);

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
                    Id = p.Id,
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
        //<param name="parteId">identificador de la pieza</param>
        ///<returns>devuelve la informacion de la pieza o vacio</returns>
        public ParteDTO GetParteById(Guid parteId)
        {
            try
            {
                return _context.Partes
                .Where(p => p.Id == parteId)
                .Select(p => new ParteDTO
                {
                    Id = p.Id,
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
        public Guid RegisterParte(Parte parte)
        {
            try
            {
                _context.Partes.Add(parte);
                _context.DbContext.SaveChanges();
                return parte.Id;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar la parte", ex);
            }
            
        }
    }
}