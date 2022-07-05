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
    /// Clase que representa el controlador de incidentes, muestra los endpoints de listado de asegurados, registro y busqueda individual
    /// </summary>
    [ApiController]
    [Route("asegurado")]
    public class AseguradoController: Controller
    {
        private readonly IAseguradoDAO _aseguradoDAO;
        private readonly IAseguradoLogic _aseguradoLogic;
        private readonly ILogger<AseguradoController> _logger;

        public AseguradoController(ILogger<AseguradoController> logger, IAseguradoDAO aseguradoDAO, IAseguradoLogic aseguradoLogic)
        {
            _aseguradoDAO = aseguradoDAO;
            _aseguradoLogic = aseguradoLogic;
            _logger = logger;
        }

    
        /// <summary>
        /// Obtiene una lista de asegurados registrados en el sistema
        /// </summary>
        /// <returns>Lista de asegurados</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<AseguradoDTO>> GetAsegurados()
        {
            var response = new ApplicationResponse<List<AseguradoDTO>>();
            try
            {

                response.Data = _aseguradoDAO.GetAsegurados();
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
        
        /// <summary>
        /// Obtiene un asegurado segun su Id
        /// </summary>
        /// <param name="id">Id del asegurado</param>
        /// <returns>Asegurado</returns>
        [HttpGet("buscar_por/{aseguradoId}")]
        public ApplicationResponse<AseguradoDTO> GetAsegurado([Required][FromRoute] Guid aseguradoId)
        {
            var response = new ApplicationResponse<AseguradoDTO>();
            try
            {
                response.Data = _aseguradoDAO.GetAseguradoByGuid(aseguradoId);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
        
        /// <summary>
        /// Obtiene un asegurado segun su nombre y apellido
        /// </summary>
        /// <param name="nombre">Nombre del asegurado</param>
        /// <param name="apellido">Apellido del asegurado</param>
        /// <returns>Asegurado</returns>
        [HttpGet("asegurados/{nombre}/{apellido}")]
        public ApplicationResponse<List<AseguradoDTO>> GetAseguradosPorNombreYApellido([Required][FromRoute] string nombre, string apellido)
        {
            var response = new ApplicationResponse<List<AseguradoDTO>>();
            try
            {
                response.Data = _aseguradoDAO.GetAseguradosPorNombreCompleto(nombre,apellido);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        /// <summary>
        /// Registra un asegurado en el sistema
        /// </summary>
        /// <param name="aseguradoDTO">Asegurado a registrar</param>
        /// <returns>Asegurado registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> AddAsegurado([FromBody] AseguradoRegisterDTO asegurado)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _aseguradoLogic.RegisterAsegurado(asegurado);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Asegurado registrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadGateway;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }

    }
}