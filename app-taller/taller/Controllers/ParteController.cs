using Microsoft.AspNetCore.Mvc;
using taller.Conections.APIs;
using taller.BussinesLogic.DTOs;
using taller.Persistence.Entities;
using taller.Persistence.DAOs;
using taller.Responses;
using taller.Exceptions;
using System.ComponentModel.DataAnnotations;


namespace taller.Controllers
{
    [ApiController]
    [Route("Partes")]
    public class ParteController : Controller
    {
        private readonly IParteDAO _ParteDAO;
        private readonly ILogger<ParteController> _logger;

        public ParteController(ILogger<ParteController> logger, IParteDAO ParteDAO)
        {
            _ParteDAO = ParteDAO;
            _logger = logger;
        }

        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<ParteDTO>> ConsultarPartes()
        {
            var response = new ApplicationResponse<List<ParteDTO>>();
            try
            {
                response.Data = _ParteDAO.GetPartes();
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

        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarParte([FromBody] ParteDTO parte)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _ParteDAO.RegisterParte(parte);
                response.Success = true;
                response.Message = "Parte registrado";
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
