using Microsoft.EntityFrameworkCore;
using administracion.DataAccess.Database;
using administracion.DataAccess.Entities;
using administracion.Exceptions;
using administracion.BussinesLogic.DTOs;

namespace administracion.DataAccess.DAOs
{
    public class VehiculoDAO: IVehiculoDAO
    {
        private static DesignTimeDbContextFactory desing = new DesignTimeDbContextFactory();

        private IAdminDBContext _context = desing.CreateDbContext(null);

        /// <summary>
        /// Pide la inforamcion de todos los vehiculos registrados
        /// </summary>
        /// <returns>Lista de DTOs con la informacion de los vehiculos</returns>
        public List<VehiculoDTO> GetAllVehiculos()
        {
            try
            {
                var data =  _context.Vehiculos
                .Include(v => v.asegurado)
                .Include(v => v.polizas)
                .Include(v => v.asegurado)
                .Select( v=> new VehiculoDTO{
                    Id = v.Id,
                    anioModelo = v.anioModelo,
                    fechaCompra = v.fechaCompra,
                    placa = v.placa!,
                    color = v.color.ToString(),
                    marca = v.marca.ToString(),
                    asegurado = (v.aseguradoId != null)? new AseguradoDTO{
                        Id = v.asegurado!.Id,
                        nombre = v.asegurado.nombre,
                        apellido = v.asegurado.apellido
                    }: null,
                    polizas = v.polizas!.Select( p => new PolizaDTO{
                        Id = p.Id,
                        fechaRegistro = p.fechaRegistro,
                        fechaVencimiento = p.fechaVencimiento,
                        tipoPoliza = p.tipoPoliza.ToString(),
                        vehiculoId = v.Id,
                    }).ToList()
                });

                return data.ToList();
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
                var data =  _context.Vehiculos
                .Include(v => v.asegurado)
                .Where(v => v.Id == vehiculoId)
                .Select(v => new VehiculoDTO
                {
                    Id = v.Id,
                    anioModelo = v.anioModelo,
                    fechaCompra = v.fechaCompra,
                    placa = v.placa!,
                    color = v.color.ToString(),
                    marca = v.marca.ToString(),
                    asegurado = (v.aseguradoId == null)
                    ? null
                    : new AseguradoDTO{
                        Id = v.asegurado!.Id,
                        nombre = v.asegurado.nombre,
                        apellido = v.asegurado.apellido
                    },
                    polizas = v.polizas!.Select( p => new PolizaDTO{
                        Id = p.Id,
                        fechaRegistro = p.fechaRegistro,
                        fechaVencimiento = p.fechaVencimiento,
                        tipoPoliza = p.tipoPoliza.ToString(),
                        vehiculoId = v.Id,
                    }).ToList()

                });  
                
                return data.First();   
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
                        Id = v.Id,
                        anioModelo = v.anioModelo,
                        fechaCompra = v.fechaCompra,
                        placa = v.placa!,
                        color = v.color.ToString(),
                        marca = v.marca.ToString(),
                        asegurado = (v.aseguradoId != null)? new AseguradoDTO{
                            Id = v.asegurado!.Id,
                            nombre = v.asegurado.nombre,
                            apellido = v.asegurado.apellido
                        }: null,
                        polizas = v.polizas!.Select( p => new PolizaDTO{
                            Id = p.Id,
                            fechaRegistro = p.fechaRegistro,
                            fechaVencimiento = p.fechaVencimiento,
                            tipoPoliza = p.tipoPoliza.ToString(),
                            vehiculoId = v.Id,
                        }
                        ).ToList()

                    });
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
        public int RegisterVehiculo(Vehiculo vehiculo)
        {
            try{

                _context.Vehiculos.Add(vehiculo);
                return _context.DbContext.SaveChanges();

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
        public int AddAsegurado(Guid vehiculoId , Guid  aseguradoId)
        {
            try
            {   
                Vehiculo vehiculo = _context.Vehiculos
                    .Where(v => v.Id == vehiculoId)
                    .First();
                vehiculo.aseguradoId = aseguradoId;
                _context.Vehiculos.Update(vehiculo);
                return _context.DbContext.SaveChanges();
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