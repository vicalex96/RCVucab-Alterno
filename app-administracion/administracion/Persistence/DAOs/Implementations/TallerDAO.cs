using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;
using administracion.Conections.rabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace administracion.Persistence.DAOs
{
    public class TallerDAO : ITallerDAO
    {
        public readonly IAdminDBContext _context;

        public TallerDAO(IAdminDBContext context)
        {
            _context = context;
        }
        public TallerDTO GetTallerByGuid(Guid tallerId)
        {
            try
            {
                var data = _context.Talleres
                .Where(t => t.tallerId == tallerId)
                .Include(t => t.marcas)
                .Select(t => new TallerDTO
                {
                    Id = t.tallerId,
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
                    throw new Exception("No se encontre algun taller con ese nombre y apellido Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ha ocurrido un error al intentar obtener el asegurado:", ex.Message, ex);
            }

        }
        public List<TallerDTO> GetTalleres()
        {
            try
            {

                var data = _context.Talleres
                .Include(t => t.marcas)
                .Select(t => new TallerDTO
                {
                    Id = t.tallerId,
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
                    throw new Exception("No se encontro ningun taller, Error 404");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new RCVException("Ocurrio un error al trata de obtener los Talleres:", ex, ex.Message, "500");
            }
        }
        
        public Guid RegisterTaller(TallerSimpleDTO taller)
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
                    tallerId = taller.Id,
                    nombreLocal = taller.nombreLocal,
                };
                _context.Talleres.Add(tallerEntity);
                _context.DbContext.SaveChanges();
                return taller.Id;
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

        public bool AddMarca(Guid tallerId, string marcaStr="-", bool todasLasMarcas = false)
        {
            try
            {
                Marca marca = new Marca();
                MarcaTaller marcaEntity;

                if(todasLasMarcas == true)
                {
                    DeleteMarcasFromTaller(tallerId);
                    marcaEntity = new MarcaTaller
                    {
                        marcaId = Guid.NewGuid(),
                        tallerId = tallerId,
                        manejaTodas = false,
                        marca = marca
                    };
                }
                else
                {
                    if(MarcaTaller.IsMarca(marcaStr))
                    {
                        marca = MarcaTaller.ConvertToMarca(marcaStr);
                    } else
                    {
                        throw new RCVException("La marca introducida no es valida");
                    }
                    var marcas = _context.MarcasTaller
                        .Where(v => v.tallerId == tallerId
                            && (v.manejaTodas == true || v.marca == marca))
                        .ToList();

                    if (marcas.Count() != 0)
                    {
                        throw new RCVException("La marca que se intenta registrar al taller ya lo esta");
                    }

                    marcaEntity = new MarcaTaller
                    {
                        marcaId = Guid.NewGuid(),
                        tallerId = tallerId,
                        manejaTodas = todasLasMarcas,
                    };
                }

                _context.MarcasTaller.Add(marcaEntity);
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
    
        private void DeleteMarcasFromTaller(Guid tallerId)
        {
            try
            {
                var data = _context.MarcasTaller
                    .Where(v => v.tallerId == tallerId);

                _context.MarcasTaller
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