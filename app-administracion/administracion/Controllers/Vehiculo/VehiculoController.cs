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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        [HttpGet("buscar_por/{guid}")]
        public ApplicationResponse<VehiculoDTO> GetVehiculoByGuid([Required][FromRoute] Guid guid)
        {
            var response = new ApplicationResponse<VehiculoDTO>();
            try
            {
                response.Data = _vehiculoDao.GetVehiculoByGuid(guid);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}