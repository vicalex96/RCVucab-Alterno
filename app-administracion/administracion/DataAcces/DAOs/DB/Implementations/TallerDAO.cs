using Microsoft.EntityFrameworkCore;
using administracion.DataAccess.Database;
using administracion.DataAccess.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.Enums;

namespace administracion.DataAccess.DAOs
{
    public class TallerDAO : ITallerDAO
    {
        private static DesignTimeDbContextFactory desing = new DesignTimeDbContextFactory();

        private IAdminDBContext _context = desing.CreateDbContext(null);
        
        /// <summary>
        /// Obtiene un Taller según su Id
        /// </summary>
        /// <param name="id">Id del Taller</param>
        /// <returns>DTO con la informacion del Taller</returns>
        public TallerDTO GetTallerByGuid(Guid tallerId)
        {
            try
            {
                var data = _context.Talleres
                .Where(t => t.Id == tallerId)
                .Include(t => t.marcas)
                .Select(t => new TallerDTO
                {
                    Id = t.Id,
                    nombreLocal = t.nombreLocal,
                    marcas = t.marcas!
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marcaName.ToString()!
                    }
                    ).ToList(),
                }).SingleOrDefault();
                if (data == null)
                {
                    throw new Exception("No se encontre algun taller con ese nombre y apellido Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ha ocurrido un error al intentar obtener el asegurado:", ex.Message, ex);
            }

        }
        
        /// <summary>
        /// Obtiene todos los taller registrados
        /// </summary>
        /// <returns>Lista de DTOs con la informacion de los talleres</returns>
        public List<TallerDTO> GetTalleres()
        {
            try
            {

                var data = _context.Talleres
                .Include(t => t.marcas)
                .Select(t => new TallerDTO
                {
                    Id = t.Id,
                    nombreLocal = t.nombreLocal,
                    marcas = t.marcas!
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marcaName.ToString()!
                    }
                    ).ToList(),
                }).ToList();
                if (data.Any() == false)
                {
                    throw new Exception("No se encontro ningun taller, Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ocurrio un error al trata de obtener los Talleres:", ex, ex.Message, "500");
            }
        }
        
        /// <summary>
        /// Registra un taller en el sistema
        /// </summary>
        /// <param name="taller">DTO de registro con la informacion del taller</param>
        /// <returns>Guid con el Id del taller</returns>
        public int  RegisterTaller(Taller taller)
        {
            try
            {
                if(taller.nombreLocal.ToLower() == "string" || 
                    taller.nombreLocal.Count() == 0)
                {
                    throw new Exception("Error campos no validos");
                }
                Taller tallerEntity = new Taller
                {
                    Id = taller.Id,
                    nombreLocal = taller.nombreLocal,
                };
                _context.Talleres.Add(tallerEntity);
                return _context.DbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new RCVException("Error al guardar, llave duplicada");
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al crear el asegurado", ex);

            }
        }

        /// <summary>
        /// Revisa si la marca existe como especializacion en el taller
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <param name="marca">Marca a revisar</param>
        /// <returns>True si existe, False si no existe</returns>
        public bool IsMarcaExistsOnTaller(Guid tallerId, MarcaName marca)
        {
            try
            {
                var data = _context.MarcasTaller
                .Where(m => m.tallerId == tallerId 
                    && (m.marcaName == marca || m.manejaTodas == true))
                .FirstOrDefault();
                return data != null? true : false;
            }
            catch (Exception ex)
            {
                throw new RCVException("Fallo al intenta buscar la existencia de la marca",ex);
            }
        }

        /// <summary>
        /// Agrega una marca a un taller existente o indica todas las marcas
        /// al indicar todas se borran los registros y se deja uno con el todasLasMarcas= true
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <param name="marca">DTO con la informacion de la marca</param>
        /// <param name="todasLasMarcas"> true si se manejan todas las marcas</param>
        /// <returns>Booleano True si se realizo bien</returns>
        public int AddMarca(MarcaTaller marca)
        {
            try
            {
                _context.MarcasTaller.Add(marca);
                return _context.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al crear el asegurado", ex);
            }
        }
    
        /// <summary>
        /// Elimina todas las marcas de un taller
        /// </summary>
        /// <param name="tallerId"></param>
        /// <returns> void </returns>
        public int DeleteMarcasFromTaller(Guid tallerId)
        {
            try
            {
                var data = _context.MarcasTaller
                    .Where(v => v.tallerId == tallerId);

                _context.MarcasTaller.RemoveRange(data.ToList());
                return _context.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RCVException("Falló al intenta borrar las marcas",ex);
            }
        }
    
    }
}