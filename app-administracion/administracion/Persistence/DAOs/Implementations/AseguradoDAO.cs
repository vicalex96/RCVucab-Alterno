using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace administracion.Persistence.DAOs
{
    public class AseguradoDAO: IAseguradoDAO
    {
        public readonly IAdminDBContext _context;

        public AseguradoDAO( IAdminDBContext context)
        {
            _context = context;
        }

        public bool RegisterAsegurado(AseguradoSimpleDTO asegurado){
            try{
                if(asegurado.nombre.ToLower() == "string" || 
                    asegurado.nombre.Count() == 0 && 
                    asegurado.apellido.ToLower() == "string" || 
                    asegurado.apellido.Count() == 0 )
                {
                    throw new Exception("Error campos no validos");
                }
                Asegurado aseguradoEntity = new Asegurado{
                    aseguradoId = asegurado.Id, 
                    nombre = asegurado.nombre, 
                    apellido = asegurado.apellido
                };
                _context.Asegurados.Add(aseguradoEntity);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(DbUpdateException)
            {
                throw new RCVException("Error identificador duplicado");
            }
            catch(Exception ex){
                throw new RCVException("Error al crear el asegurado", ex);
                
            }
        }

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
        public AseguradoDTO GetAseguradoByGuid(Guid Id)
        {
            try
            {
                AseguradoDTO data = _context.Asegurados
                .Where(p => p.aseguradoId == Id)
                .Select( b=> new AseguradoDTO{
                    Id = b.aseguradoId,
                    nombre = b.nombre,
                    apellido = b.apellido
                }).FirstOrDefault()!;
                if(data == null)
                    throw new RCVException("No se encontró algún asegurado con el indentificador especificado");

                return data;
            }
            catch (RCVException ex) {
                throw new RCVException(ex.Mensaje,ex);
            }
            catch (Exception ex) {
                throw new RCVException("Error al intentar obtener al asegurado", ex);
            }
        }
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