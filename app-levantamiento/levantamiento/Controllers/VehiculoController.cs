using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Exceptions;
using levantamiento.Responses;
using System.ComponentModel.DataAnnotations;
using levantamiento.Conections.APIs;

namespace administracion.Controllers
{
    [ApiController]
    [Route("Vehiculo")]
    public class VehiculoController: Controller
    {
        private readonly ILogger<VehiculoController> _logger;

        public VehiculoController(ILogger<VehiculoController> logger)
        {
            _logger = logger;
        }
        [HttpGet("buscar_por/{VehiculoId}")]
        public async Task<ApplicationResponse<VehiculoDTO>> GetVehiculoById(Guid VehiculoId)
        {
            var response = new ApplicationResponse<VehiculoDTO>();
            try
            {
                VehiculoAPI vehiculoAPI = new VehiculoAPI();
                response.Data = await vehiculoAPI.GetVehiculoFromAdmin(VehiculoId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Vehiculo encontrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
            
        [HttpPost("registrar")]
        public async Task<ApplicationResponse<bool>> RegisterVehiculo([Required][FromBody] VehiculoRegisterDTO vehiculo)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                VehiculoAPI vehiculoAPI = new VehiculoAPI();
                response.Data = await vehiculoAPI.RegisterVehiculo(vehiculo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Vehiculo registrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        
    }
}