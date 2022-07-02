using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que contiene los endpoints de los vehiculos
    /// </summary>
    [ApiController]
    [Route("vehiculo")]
    public class VehiculoController: Controller
    {
        private readonly IVehiculoDAO _vehiculoDao;
        private readonly ILogger<VehiculoController> _logger;

        public VehiculoController(ILogger<VehiculoController> logger, IVehiculoDAO vehiculoDao)
        {
            _vehiculoDao = vehiculoDao;
            _logger = logger;
        }

        /// <summary>
        /// Muestra la lista de vehiculos registrados en el sistema
        /// </summary>
        /// <returns>Lista de vehiculos</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<VehiculoDTO>> GetAllVehiculos()
        {
            var response = new ApplicationResponse<List<VehiculoDTO>>();
            try
            {
                response.Data = _vehiculoDao.GetAllVehiculos();
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Muestra la informacion de un vehiculo indicado por el Id
        /// </summary>
        /// <param name="vehiculoId">Id del vehiculo</param>
        /// <returns>Vehiculo</returns>
        [HttpGet("buscar_por/{vehiculoId}")]
        public ApplicationResponse<VehiculoDTO> GetVehiculoByGuid([Required][FromRoute] Guid vehiculoId)
        {
            var response = new ApplicationResponse<VehiculoDTO>();
            try
            {
                response.Data = _vehiculoDao.GetVehiculoByGuid(vehiculoId);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        /// <summary>
        /// Crea un nuevo vehiculo en el sistema
        /// </summary>
        /// <param name="vehiculo">Vehiculo a crear</param>
        /// <returns>Vehiculo registrado</returns>
        [HttpPost("crear")]
        public ApplicationResponse<bool> createVehiculo([FromBody] VehiculoSimpleDTO Vehiculo)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _vehiculoDao.RegisterVehiculo(Vehiculo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = "Vehiculo registrado correctamente";
                response.Success = true;
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Actualiza la informacion de un vehiculo para agregarle asegurado
        /// </summary>
        /// <param name="vehiculoId">Id Vehiculo a actualizar</param>
        /// <param name="aseguradoId">Id asegurado a asociar</param>
        /// <returns>Vehiculo actualizado</returns>
        [HttpPost("asociar_asegurado/{vehiculoId}/{aseguradoId}")]
        public ApplicationResponse<bool> AddAsegurado([Required][FromRoute] Guid vehiculoId ,[Required][FromRoute] Guid aseguradoId)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _vehiculoDao.AddAsegurado(vehiculoId, aseguradoId );
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}