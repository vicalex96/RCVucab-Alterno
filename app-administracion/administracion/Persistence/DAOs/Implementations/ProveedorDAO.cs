using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;
using administracion.Conections.rabbit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace administracion.Persistence.DAOs
{
    public class ProveedorDAO : IProveedorDAO
    {
        public readonly IAdminDBContext _context;

        public ProveedorDAO(IAdminDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra un proveedor nuevo en la base de datos
        /// </summary>
        /// <param name="proveedor">DTO de regsitro con la informacion del proveedor</param>
        /// <returns>Guid con el Id del proveedor</returns>
        public Guid RegisterProveedor(Proveedor proveedor)
        {
            try
            {
                _context.Proveedores.Add(proveedor);
                _context.DbContext.SaveChanges();
                return proveedor.proveedorId;
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
        /// Obtiene un proveedor según su Id
        /// </summary>
        /// <param name="id">Id del proveedor</param>
        /// <returns>DTO con la informacion del proveedor</returns>
        public ProveedorDTO GetProveedorByGuid(Guid proveedorId)
        {
            try
            {
                var data = _context.Proveedores
                .Where(t => t.proveedorId == proveedorId)
                .Include(t => t.marcas)
                .Select(t => new ProveedorDTO
                {
                    Id = t.proveedorId,
                    nombreLocal = t.nombreLocal,
                    marcas = t.marcas!
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marca.ToString()!
                    }
                    ).ToList(),
                }).SingleOrDefault();
                if (data == null)
                {
                    throw new Exception("No se encontre algun Proveedor con ese nombre y apellido Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ha ocurrido un error al intentar obtener el asegurado:", ex.Message, ex);
            }

        }

        /// <summary>
        /// Obtiene todos los proveedores registrados en  el sistema
        /// </summary>
        /// <returns>Lista de DTOs con la informacion de los proveedores</returns>
        public List<ProveedorDTO> GetProveedores()
        {
            try
            {
                var data = _context.Proveedores
                .Include(t => t.marcas)
                .Select(t => new ProveedorDTO
                {
                    Id = t.proveedorId,
                    nombreLocal = t.nombreLocal,
                    marcas = t.marcas!
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marca.ToString()!
                    }
                    ).ToList(),
                }).ToList();
                if (data.Any() == false)
                {
                    throw new Exception("No se encontro ningun Proveedor, Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ocurrio un error al trata de obtener los Proveedores:", ex, ex.Message, "500");
            }
        }
        
        /// <summary>
        /// Revisa si la marca existe como especializacion en el taller
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <param name="marca">Marca a revisar</param>
        /// <returns>True si existe, False si no existe</returns>
        public bool IsMarcaExistsOnProveedor(Guid proveedorId, Marca marca)
        {
            try
            {
                var data = _context.MarcasProveedor
                .Where(m => m.proveedorId == proveedorId 
                    && (m.marca == marca || m.manejaTodas == true))
                .FirstOrDefault();
                return data != null? true : false;
            }
            catch(ArgumentNullException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Agrega una marca a un taller existente o indica todas las marcas
        /// al indicar todas se borran los registros y se deja uno con el todasLasMarcas= true
        /// </summary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <param name="marca">DTO con la informacion de la marca</param>
        /// <param name="todasLasMarcas"> true si se manejan todas las marcas</param>
        /// <returns>Booleano True si se realizo bien</returns>
        public bool AddMarca(MarcaProveedor marca)
        {
            try
            {
                _context.MarcasProveedor.Add(marca);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al crear el asegurado", ex);
            }
        }
        
        /// <summary>
        /// Elimina todas las marcas de un proveedor
        /// </summary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <returns>Void</returns>
        public bool DeleteMarcasFromProveedor(Guid proveedorId)
        {
            try
            {
                var data = _context.MarcasProveedor
                    .Where(v => v.proveedorId == proveedorId);

                _context.MarcasProveedor
                    .RemoveRange(data.ToList());
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new RCVException("Fallo al intenta borrar las marcas",ex);
            }
        }
    }
}