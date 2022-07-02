using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Persistence.DAOs;
using levantamiento.Exceptions;
using levantamiento.Responses;

namespace administracion.Controllers
{
    [ApiController]
    [Route("Parte")]
    public class ParteController: Controller
    {
        private readonly IParteDAO _ParteDAO;
        private readonly ILogger<ParteController> _logger;

        public ParteController(ILogger<ParteController> logger, IParteDAO ParteDAO)
        {
            _logger = logger;
            _ParteDAO = ParteDAO;
        }

        /// <summary>
        /// Obtiene todas los partes registradas
        /// </summary>
        /// <returns>Lista de partes</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<ParteDTO>> GetAll()
        {
            var response = new ApplicationResponse<List<ParteDTO>>();
            try
            {
                response.Data = _ParteDAO.GetAll();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Requerimiento registrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Mensaje;
                response.Success = false;
            }
            return response;
        } 


        /// <summary>
        ///Registrar una parte nueva en el sistema
        /// </summary>
        /// <param name="Parte">Objeto ParteDTO con los datos de la parte</param>
        /// <returns>Objeto Response con el resultado de la operación</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegisterParte([FromBody] ParteDTO Parte)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _ParteDAO.RegisterParte(Parte);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Parte registrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Mensaje;
                response.Success = false;
            }
            return response;
        }
        
    }
}