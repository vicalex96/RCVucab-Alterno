using Microsoft.EntityFrameworkCore;
using administracion.DataAccess.Database;
using administracion.DataAccess.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;

namespace administracion.DataAccess.DAOs
{
    public class AseguradoDAO: IAseguradoDAO
    {
        private static DesignTimeDbContextFactory desing = new DesignTimeDbContextFactory();

        private IAdminDBContext _context = desing.CreateDbContext(null);

        /// <summary>
        /// Registra un asegurado nuevo
        /// </summary>
        /// <param name="asegurado">DTO con la informacion del asegurado</param>
        /// <returns>booleano true</returns>
        public int RegisterAsegurado(Asegurado asegurado){
            try{
                _context.Asegurados.Add(asegurado);
                return _context.DbContext.SaveChanges();
            }
            catch(Exception ex){
                throw new RCVException("Error al crear el asegurado", ex);
                
            }
        }

        /// <summary>
        /// Obtiene una lista de asegurados
        /// </summary>
        /// <returns>Lista de asegurados</returns>
        public List<AseguradoDTO> GetAsegurados()
        {
            try
            {
                var asegurados = _context.Asegurados
                .Include(a => a.vehiculos)
                .Select( a=> new AseguradoDTO{
                    Id = a.Id,
                    nombre = a.nombre,
                    apellido = a.apellido,
                    vehiculos = a.vehiculos!.Select( v => new VehiculoDTO{
                        Id = v.Id,
                        anioModelo = v.anioModelo,
                        fechaCompra = v.fechaCompra,
                        placa = v.placa!,
                        color = v.color.ToString(),
                        marca = v.marca.ToString()
                    }).ToList()
                });
                if(asegurados.ToList().Count == 0){
                    throw new Exception("No se encontraron vehiculos con ese nombre y apellido Error 404");
                }
                return asegurados.ToList();

            }
            catch(Exception ex)
            {
                throw new RCVException("Ha ocurrido un error al intentar consultar la lista de asegurados", ex.Message, ex);
            }
        }
        
        /// <summary>
        /// Obtiene la informacion de un asegurado
        /// </summary>
        /// <param name="id">Identificador del asegurado</param>
        /// <returns>DTO con la informacion del asegurado</returns>
        public AseguradoDTO GetAseguradoByGuid(Guid aseguradoId)
        {
            try
            {
                AseguradoDTO data = _context.Asegurados
                .Where(p => p.Id == aseguradoId)
                .Select( b=> new AseguradoDTO{
                    Id = b.Id,
                    nombre = b.nombre,
                    apellido = b.apellido
                }).First();

                return data;
            }
            catch (RCVException ex) {
                throw new RCVException(ex.Mensaje,ex);
            }
            catch (Exception ex) {
                throw new RCVException("Error al intentar obtener al asegurado", ex);
            }
        }
        
        /// <summary>
        /// Busca asegurados según su nombre
        /// </summary>
        /// <param name="nombre">Nombre del asegurado</param>
        /// <returns>Lista de asegurados</returns>
        public List<AseguradoDTO> GetAseguradosPorNombreCompleto(string nombre, string apellido)
        {
            try
            {
                var data = _context.Asegurados.Where(p => p.apellido.Contains(apellido) == true && p.nombre.Contains(nombre) == true).Select( b=> new AseguradoDTO{
                    Id = b.Id,
                    nombre = b.nombre,
                    apellido = b.apellido
                });
                if(data.ToList().Count == 0){
                    Console.WriteLine("\n Lanzando error");
                    throw new Exception("No se encontraron asegurados con ese nombre y apellido Error 404");
                }
                return data.ToList();
            }
            catch (Exception ex) {

                throw new RCVException("Ha ocurrido un error al intentar obtener el asegurado:", ex.Message, ex);
            }
        }
    }
}