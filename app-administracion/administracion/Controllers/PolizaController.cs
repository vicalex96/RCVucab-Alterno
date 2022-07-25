using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using administracion.BussinesLogic.Commands;

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
        private readonly ILogger<PolizaController> _logger;

        public PolizaController(ILogger<PolizaController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Resgistra una poliza en el sistema
        /// </summary>
        /// <param name="polizaDTO">Poliza a registrar</param>
        /// <returns>Poliza registrada</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<int> registrarPoliza([FromBody] PolizaRegisterDTO poliza)
        {
            var response = new ApplicationResponse<int>();
            try
            {
                RegisterPolizaCommand command = PolizaCommandFactory
                    .createRegisterPolizaCommand(poliza);

                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Poliza registrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
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
        [HttpGet("consultar/{polizaId}")]
        public ApplicationResponse<PolizaDTO> consultarPolizaById([Required][FromRoute] Guid polizaId)
        {
            var response = new ApplicationResponse<PolizaDTO>();
            try
            {
                GetPolizaByIdCommand command = PolizaCommandFactory
                    .createGetPolizaByIdCommand(polizaId);

                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Poliza encontrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
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
                GetPolizaByVehiculoIdCommand command = PolizaCommandFactory
                    .createGetPolizaByVehiculoIdCommand(vehiculoID);
                
                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Poliza encontrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }
    }
}