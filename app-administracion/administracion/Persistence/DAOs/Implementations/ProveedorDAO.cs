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

        public bool RegisterProveedor(ProveedorSimpleDTO proveedor)
        {
            try
            {
                if(proveedor.nombreLocal.ToLower() == "string" || 
                    proveedor.nombreLocal.Count() == 0)
                {
                    throw new Exception("Error");
                }
                Proveedor proveedorEntity = new Proveedor
                {
                    proveedorId = proveedor.Id,
                    nombreLocal = proveedor.nombreLocal,
                };
                _context.Proveedores.Add(proveedorEntity);
                _context.DbContext.SaveChanges();
                /*
                ProductorRabbit rabbit = new ProductorRabbit();
                rabbit.SendMessage(Routings.taller,"registrar_provedor", proveedor.Id.ToString());
                */
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new RCVException("Error al guardar, llave duplicada");
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al crear el asegurado", ex);

            }
        }
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
                    marcas = t.marcas
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marca.ToString()
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
                    marcas = t.marcas
                    .Select(m => new MarcaDTO
                    {
                        nombreMarca = m.manejaTodas ? "TodasLasMarcas" : m.marca.ToString()
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

        public bool AddMarca(Guid proveedorId, string marcaStr="-", bool todasLasMarcas = false)
        {
            try
            {
                Marca marca = new Marca();
                MarcaProveedor marcaEntity;

                if(todasLasMarcas == true)
                {
                    DeleteMarcasFromProveedor(proveedorId);
                    marcaEntity = new MarcaProveedor
                    {
                        marcaId = Guid.NewGuid(),
                        proveedorId = proveedorId,
                        manejaTodas = false,
                        marca = marca
                    };
                }
                else
                {
                    if(MarcaProveedor.IsMarca(marcaStr))
                    {
                        marca = MarcaProveedor.ConvertToMarca(marcaStr);
                    } else
                    {
                        throw new RCVException("La marca introducida no es valida");
                    }
                    var marcas = _context.MarcasProveedor
                        .Where(v => v.proveedorId == proveedorId
                            && (v.manejaTodas == true || v.marca == marca))
                        .ToList();

                    if (marcas.Count() != 0)
                    {
                        throw new RCVException("La marca que se intenta registrar al taller ya lo esta");
                    }

                    marcaEntity = new MarcaProveedor
                    {
                        marcaId = Guid.NewGuid(),
                        proveedorId = proveedorId,
                        manejaTodas = todasLasMarcas,
                    };
                }

                _context.MarcasProveedor.Add(marcaEntity);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch (RCVException ex)
            {
                throw new RCVException(ex.Mensaje);
            }
            catch (ArgumentNullException ex)
            {
                throw new RCVException("Error no se encontró ningun taller con el Guid indicado", ex, ex.Message, "404");
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al crear el asegurado", ex);

            }
        }
    
        private void DeleteMarcasFromProveedor(Guid proveedorId)
        {
            try
            {
                var data = _context.MarcasProveedor
                    .Where(v => v.proveedorId == proveedorId);

                _context.MarcasProveedor
                    .RemoveRange(data.ToList());
                _context.DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RCVException("Fallo al intenta borrar las marcas",ex);
            }
        }
    
    }
}