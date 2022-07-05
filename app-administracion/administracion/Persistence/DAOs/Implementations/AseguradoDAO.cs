using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    public class AseguradoDAO: IAseguradoDAO
    {
        public readonly IAdminDBContext _context;

        public AseguradoDAO( IAdminDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un asegurado nuevo
        /// </summary>
        /// <param name="asegurado">DTO con la informacion del asegurado</param>
        /// <returns>booleano true</returns>
        public bool RegisterAsegurado(Asegurado asegurado){
            try{
                _context.Asegurados.Add(asegurado);
                _context.DbContext.SaveChanges();
                return true;
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
                    Id = a.aseguradoId,
                    nombre = a.nombre,
                    apellido = a.apellido,
                    vehiculos = a.vehiculos!.Select( v => new VehiculoDTO{
                        Id = v.vehiculoId,
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
                .Where(p => p.aseguradoId == aseguradoId)
                .Select( b=> new AseguradoDTO{
                    Id = b.aseguradoId,
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
                    Id = b.aseguradoId,
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