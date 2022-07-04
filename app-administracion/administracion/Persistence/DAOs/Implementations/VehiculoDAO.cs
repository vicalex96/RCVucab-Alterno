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
    public class VehiculoDAO: IVehiculoDAO
    {
        public readonly IAdminDBContext _context;

        public VehiculoDAO( IAdminDBContext context)
        {
            _context = context;
        }

        public List<VehiculoDTO> GetAllVehiculos()
        {
            try
            {
                var vehiculos = _context.Vehiculos
                //.Include(v => v.asegurado)
                .Include(v => v.polizas)
                .Select( v=> new VehiculoDTO{
                    Id = v.vehiculoId,
                    anioModelo = v.anioModelo,
                    fechaCompra = v.fechaCompra,
                    placa = v.placa!,
                    color = v.color.ToString(),
                    marca = v.marca.ToString(),
                    asegurado =  _context.Asegurados
                    .Where(a => a.aseguradoId == v.aseguradoId)
                    .Select(a =>
                    new AseguradoDTO{
                        Id = v.asegurado!.aseguradoId,
                        nombre = v.asegurado.nombre,
                        apellido = v.asegurado.apellido
                    }).FirstOrDefault(),
                    polizas = v.polizas!.Select( p => new PolizaDTO{
                            Id = p.polizaId,
                            fechaRegistro = p.fechaRegistro,
                            fechaVencimiento = p.fechaVencimiento,
                            tipoPoliza = p.tipoPoliza.ToString(),
                            vehiculoId = v.vehiculoId,
                    }).ToList()
                }).ToList();
                Console.WriteLine(vehiculos);
                if(vehiculos.Count == 0){
                    throw new Exception("No se encontraron vehiculos con ese nombre y apellido Error 404");
                }
                return vehiculos.ToList();

            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los vehiculos", ex);
            }
        }

        public VehiculoDTO GetVehiculoByGuid(Guid Id)
        {   
            try
            {
                VehiculoDTO data = _context.Vehiculos
                    .Include(v => v.asegurado)
                    .Where(v => v.vehiculoId == Id)
                    .Select(v => new VehiculoDTO
                    {
                        Id = v.vehiculoId,
                        anioModelo = v.anioModelo,
                        fechaCompra = v.fechaCompra,
                        placa = v.placa!,
                        color = v.color.ToString(),
                        marca = v.marca.ToString(),
                        asegurado = new AseguradoDTO{
                            Id = v.asegurado!.aseguradoId,
                            nombre = v.asegurado!.nombre,
                            apellido = v.asegurado!.apellido
                        },
                        polizas = v.polizas!.Select( p => new PolizaDTO{
                            Id = p.polizaId,
                            fechaRegistro = p.fechaRegistro,
                            fechaVencimiento = p.fechaVencimiento,
                            tipoPoliza = p.tipoPoliza.ToString(),
                            vehiculoId = v.vehiculoId,
                    }).ToList()

                }).FirstOrDefault()!; 
                if(data == null)
                    throw new RCVException("No se encontró algún  vehículo con el id especificado");
                
                return data;
            }
            catch (RCVException ex) {
                throw new RCVException(ex.Mensaje,ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener el vehículo", ex);
            }


        }
        
        public List<VehiculoDTO> GetVehiculosByAsegurado(Guid aseguradoId)
        {
            try
            {
                var data = _context.Vehiculos
                    .Include(v => v.asegurado)
                    .Include(v => v.polizas)
                    .Where(v => v.aseguradoId == aseguradoId)
                    .Select(v => new VehiculoDTO
                    {
                        Id = v.vehiculoId,
                        anioModelo = v.anioModelo,
                        fechaCompra = v.fechaCompra,
                        placa = v.placa!,
                        color = v.color.ToString(),
                        marca = v.marca.ToString(),
                        asegurado =  _context.Asegurados
                        .Where(a => a.aseguradoId == v.aseguradoId)
                        .Select(a => new AseguradoDTO{
                            Id = v.asegurado!.aseguradoId,
                            nombre = v.asegurado!.nombre,
                            apellido = v.asegurado!.apellido
                        }).FirstOrDefault(),
                        polizas = v.polizas!.Select( p => new PolizaDTO{
                            Id = p.polizaId,
                            fechaRegistro = p.fechaRegistro,
                            fechaVencimiento = p.fechaVencimiento,
                            tipoPoliza = p.tipoPoliza.ToString(),
                            vehiculoId = v.vehiculoId,
                        }
                        ).ToList()

                    }).ToList();
                if(data.ToList().Count == 0){
                    throw new Exception("No se encontraron vehiculos con ese nombre y apellido Error 404");
                }
                return data.ToList();
            }
            catch (Exception ex) {
                throw new RCVException("Ha ocurrido un error al intentar obtener el vehiculo:", ex.Message, ex);
            }
        }
        
        public bool RegisterVehiculo(VehiculoSimpleDTO auto)
        {
            try{
                Color _color = (Color)Enum.Parse(typeof(Color), auto.color);
                Marca _marca = (Marca)Enum.Parse(typeof(Marca), auto.marca);
                var vehiculo = new Vehiculo();
                vehiculo.vehiculoId = auto.Id;
                vehiculo.anioModelo = auto.anioModelo;
                vehiculo.fechaCompra = auto.fechaCompra;
                vehiculo.color = _color;
                vehiculo.placa = auto.placa;
                vehiculo.marca = _marca;
                _context.Vehiculos.Add(vehiculo);
                _context.DbContext.SaveChanges();

                return true;
            }
            catch(ArgumentException ex)
            {
                throw new RCVException("Error: alguno de los argumentos no es valido, color y marca deben de existir en el sistema, la placa tiene un maximo de 7 caracteres", ex);
            }
            catch(Exception ex){
                throw new RCVException("Error: Se genero un error desconcido, recibe bien los datos suministrados y vuelva a intentar", ex);
            }
        }

        public bool AddAsegurado(Guid vehiculoId , Guid  aseguradoId)
        {
            try
            {
                Vehiculo vehiculo = _context.Vehiculos
                    .Where(v => v.vehiculoId == vehiculoId)
                    .First()!;
                
                Asegurado asegurado = _context.Asegurados
                    .Where(v => v.aseguradoId == aseguradoId)
                    .First();
                vehiculo.aseguradoId = aseguradoId;
                _context.Vehiculos.Update(vehiculo);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(DbUpdateException ex)
            {
                throw new RCVException("Error: El vehiculo o el asegurado no existe", ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("Ocurrio un error desconocido al intentar agregar el asegurado", ex);
            }
        }
    }
}