using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using administracion.BussinesLogic.LogicClasses;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que contiene los endpoints de la la poliza
    /// </summary>
    [ApiController]
    [Route("poliza")]
    public class PolizaController: Controller
    {
        private readonly IPolizaDAO _polizaDao;
        private readonly IPolizaLogic _polizaLogic;
        private readonly ILogger<PolizaController> _logger;

        public PolizaController(ILogger<PolizaController> logger, IPolizaDAO polizaDao, IPolizaLogic polizaLogic)
        {
            _polizaDao = polizaDao;
            _polizaLogic = polizaLogic;
            _logger = logger;
        }

        /// <summary>
        /// Resgistra una poliza en el sistema
        /// </summary>
        /// <param name="polizaDTO">Poliza a registrar</param>
        /// <returns>Poliza registrada</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> registrarPoliza([FromBody] PolizaRegisterDTO poliza)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data  = _polizaLogic.RegisterPoliza(poliza);
                response.Success = true;
                response.Message = "Poliza registrada";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Busca la poliza vigente en el sistema según el Id del vehiculo
        /// </summary>
        /// <param name="vehiculoId">Id del vehiculo</param>
        /// <returns>Poliza</returns>
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
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }
    }
}