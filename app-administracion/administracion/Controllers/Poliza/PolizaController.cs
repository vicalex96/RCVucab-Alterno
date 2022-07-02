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
    [Route("poliza")]
    public class PolizaController: Controller
    {
        private readonly IPolizaDAO _polizaDao;
        private readonly ILogger<PolizaController> _logger;

        public PolizaController(ILogger<PolizaController> logger, IPolizaDAO polizaDao)
        {
            _polizaDao = polizaDao;
            _logger = logger;
        }

        [HttpPost("registrar")]
        public ApplicationResponse<bool> registrarPoliza([FromBody] PolizaSimpleDTO poliza)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _polizaDao.RegisterPoliza(poliza);
                response.Message = "Poliza registrada";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpGet("consultar_por_vehiculo/{vehiculoID}")]
        public ApplicationResponse<PolizaDTO> consultarPolizaDeVehiculo([Required][FromRoute] Guid vehiculoID)
        {
            var response = new ApplicationResponse<PolizaDTO>();
            try
            {
                response.Data = _polizaDao.GetPolizaByVehiculoGuid(vehiculoID);
                response.Success = true;
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
    }
}