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
    [Route("asegurado")]
    public class AseguradoController: Controller
    {
        private readonly IAseguradoDAO _aseguradoDAO;
        private readonly ILogger<AseguradoController> _logger;

        public AseguradoController(ILogger<AseguradoController> logger, IAseguradoDAO aseguradoDAO)
        {
            _aseguradoDAO = aseguradoDAO;
            _logger = logger;
        }

    

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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        [HttpGet("buscar_por/{guid}")]
        public ApplicationResponse<AseguradoDTO> GetAsegurado([Required][FromRoute] Guid guid)
        {
            var response = new ApplicationResponse<AseguradoDTO>();
            try
            {
                response.Data = _aseguradoDAO.GetAseguradoByGuid(guid);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        
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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.Message.ToString();
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
            }
            return response;
        }

        [HttpPost("agregar")]
        public ApplicationResponse<bool> AddAsegurado([FromBody] AseguradoSimpleDTO asegurado)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _aseguradoDAO.RegisterAsegurado(asegurado);
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadGateway;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

    }
}