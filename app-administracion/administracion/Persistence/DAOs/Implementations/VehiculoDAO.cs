using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Database;
using administracion.Persistence.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    public class VehiculoDAO: IVehiculoDAO
    {
        public readonly IAdminDBContext _context;

        public VehiculoDAO( IAdminDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pide la inforamcion de todos los vehiculos registrados
        /// </summary>
        /// <returns>Lista de DTOs con la informacion de los vehiculos</returns>
        public List<VehiculoDTO> GetAllVehiculos()
        {
            try
            {
                var vehiculos = _context.Vehiculos
                .Include(v => v.asegurado)
                .Include(v => v.polizas)
                .Include(v => v.asegurado)
                .Select( v=> new VehiculoDTO{
                    Id = v.vehiculoId,
                    anioModelo = v.anioModelo,
                    fechaCompra = v.fechaCompra,
                    placa = v.placa!,
                    color = v.color.ToString(),
                    marca = v.marca.ToString(),
                    asegurado = (v.aseguradoId != null)? new AseguradoDTO{
                        Id = v.asegurado!.aseguradoId,
                        nombre = v.asegurado.nombre,
                        apellido = v.asegurado.apellido
                    }: null,
                    polizas = v.polizas!.Select( p => new PolizaDTO{
                        Id = p.polizaId,
                        fechaRegistro = p.fechaRegistro,
                        fechaVencimiento = p.fechaVencimiento,
                        tipoPoliza = p.tipoPoliza.ToString(),
                        vehiculoId = v.vehiculoId,
                    }).ToList()
                }).ToList();

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

        /// <summary>
        /// Pide la inforamcion de un vehiculo por el id del vehiculo
        /// </summary>
        /// <param name="vehiculoId">id del vehiculo</param>
        /// <returns>DTO con la informacion del vehiculo</returns>
        public VehiculoDTO GetVehiculoByGuid(Guid vehiculoId)
        {   
            try
            {
                VehiculoDTO data = _context.Vehiculos
                .Include(v => v.asegurado)
                .Where(v => v.vehiculoId == vehiculoId)
                .Select(v => new VehiculoDTO
                {
                    Id = v.vehiculoId,
                    anioModelo = v.anioModelo,
                    fechaCompra = v.fechaCompra,
                    placa = v.placa!,
                    color = v.color.ToString(),
                    marca = v.marca.ToString(),
                    asegurado = (v.aseguradoId == null)
                    ? null
                    : new AseguradoDTO{
                        Id = v.asegurado!.aseguradoId,
                        nombre = v.asegurado.nombre,
                        apellido = v.asegurado.apellido
                    },
                    polizas = v.polizas!.Select( p => new PolizaDTO{
                        Id = p.polizaId,
                        fechaRegistro = p.fechaRegistro,
                        fechaVencimiento = p.fechaVencimiento,
                        tipoPoliza = p.tipoPoliza.ToString(),
                        vehiculoId = v.vehiculoId,
                    }).ToList()

                }).First();     
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
        
        /// <summary>
        /// Pide el listado de los vehiculos de un asegurado
        /// </summary>
        /// <param name="aseguradoId">id del asegurado</param>
        /// <returns>Lista de DTOs con la informacion de los vehiculos</returns>
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
                        asegurado = (v.aseguradoId != null)? new AseguradoDTO{
                            Id = v.asegurado!.aseguradoId,
                            nombre = v.asegurado.nombre,
                            apellido = v.asegurado.apellido
                        }: null,
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
        
        /// <summary>
        /// Regsitra un vehiculo en el sistema
        /// </summary>
        /// <param name="vehiculo">DTO de regsitro con la informacion del vehiculo</param>
        /// <returns>Boleano true si todo salio bien</returns>
        public bool RegisterVehiculo(Vehiculo vehiculo)
        {
            try{

                _context.Vehiculos.Add(vehiculo);
                _context.DbContext.SaveChanges();

                return true;
            }
            catch(Exception ex){
                throw new RCVException("Error: Se genero un error desconcido, recibe bien los datos suministrados y vuelva a intentar", ex);
            }
        }

        /// <summary>
        /// Asocia un asegurado a un vehiculo que no tenga uno ya registrado
        /// </summary>
        /// <param name="vehiculoId">id del vehiculo</param>
        /// <param name="aseguradoId">id del asegurado</param>
        /// <returns>Boleano true si todo salio bien</returns>
        public bool AddAsegurado(Guid vehiculoId , Guid  aseguradoId)
        {
            try
            {   
                Vehiculo vehiculo = _context.Vehiculos
                    .Where(v => v.vehiculoId == vehiculoId)
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