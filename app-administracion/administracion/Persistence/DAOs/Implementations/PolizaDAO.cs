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
    public class PolizaDAO: IPolizaDAO
    {
        public readonly IAdminDBContext _context;

        public PolizaDAO( IAdminDBContext context)
        {
            _context = context;
        }


        public bool RegisterPoliza (PolizaSimpleDTO poliza)
        {
            try
            {
                Poliza polizaEntity = new Poliza();

                polizaEntity.polizaId = Guid.NewGuid();
                polizaEntity.fechaRegistro = poliza.fechaRegistro;
                polizaEntity.fechaVencimiento = poliza.fechaVencimiento;
                polizaEntity.tipoPoliza = (TipoPoliza)Enum.Parse(typeof(TipoPoliza), poliza.tipoPoliza);
                polizaEntity.vehiculoId = poliza.vehiculoId;

                _context.Polizas.Add(polizaEntity);
                _context.DbContext.SaveChanges();
                return true;
            }
            catch(Exception ex){
                throw new RCVException("Error al crear el asegurado", ex);
            }
            return false;
        }

        public PolizaDTO GetPolizaByGuid(Guid polizaId)
        {
            try
            {
                var poliza = _context.Polizas
                .Where(p => p.polizaId == polizaId)
                .Select( p=> new PolizaDTO{
                    Id = p.polizaId,
                    fechaRegistro = p.fechaRegistro,
                    fechaVencimiento = p.fechaVencimiento,
                    tipoPoliza = p.tipoPoliza.ToString(),
                    vehiculoId = p.vehiculoId
                }).FirstOrDefault();
                
                if(poliza == null){
                    throw new Exception("No se encontraron vehiculos con ese nombre y apellido Error 404");
                }
                return poliza;

            }
            catch(Exception ex)
            {
                throw new RCVException("Error no se obtuvo ninguna poliza", ex);
            }
        }

        public PolizaDTO GetPolizaByVehiculoGuid(Guid vehiculoID)
        {
            try
            {
                var poliza = _context.Polizas
                .Include(p => p.vehiculo)
                .Include(p => p.vehiculo.asegurado)
                .Where(p => p.vehiculoId == vehiculoID)
                .Select( p=> new PolizaDTO{
                    Id = p.polizaId,
                    fechaRegistro = p.fechaRegistro,
                    fechaVencimiento = p.fechaVencimiento,
                    tipoPoliza = p.tipoPoliza.ToString(),
                    vehiculoId = p.vehiculoId,
                    vehiculo = new VehiculoDTO{
                        Id = p.vehiculo.vehiculoId,
                        anioModelo = p.vehiculo.anioModelo,
                        color = p.vehiculo.color.ToString(),
                        marca = p.vehiculo.marca.ToString(),
                        asegurado =  new AseguradoDTO{
                            Id = p.vehiculo.asegurado.aseguradoId,
                            nombre = p.vehiculo.asegurado.nombre,
                            apellido = p.vehiculo.asegurado.apellido
                        }
                    }
                }).FirstOrDefault();
                if(poliza == null){
                    throw new Exception("No se encontraron vehiculos con ese nombre y apellido Error 404");
                }
                return poliza;

            }
            catch(Exception ex)
            {
                throw new RCVException("Error al obtener los vehiculos", ex);
            }
        }
    }
}