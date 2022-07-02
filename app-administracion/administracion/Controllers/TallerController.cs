using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que contiene los endpoints del trabajo con talleres
    /// </summary>
    [ApiController]
    [Route("Taller")]
    public class TallerController: Controller
    {
        private readonly ITallerDAO _TallerDao;

        private readonly ILogger<TallerController> _logger;

        public TallerController(ILogger<TallerController> logger, ITallerDAO TallerDao)
        {
            _TallerDao = TallerDao;
            _logger = logger;
        }

        /// <summary>
        /// Mostrar un listado de talleres que existen en el sistema
        /// </summary>
        /// <returns>Talleres en sistema</returns>     
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<TallerDTO>> ConsultarTalleres()
        {
            var response = new ApplicationResponse<List<TallerDTO>>();
            try
            {
                response.Data = _TallerDao.GetTalleres();
                response.Success = true;
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
        /// Busca un taller en el sistema según el Id del taller
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <returns>Taller</returns>
        [HttpGet("buscar_por/{tallerId}")]
        public ApplicationResponse<TallerDTO> ConsultarTallerPorId([Required][FromRoute] Guid tallerId)
        {
            var response = new ApplicationResponse<TallerDTO>();
            try
            {
                response.Data = _TallerDao.GetTallerByGuid(tallerId);
                response.Success = true;
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
        /// Registra un taller en el sistema
        /// </summary>
        /// <param name="tallerSimpleDTO">Taller a registrar</param>
        /// <returns>Taller registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarTaller([FromBody] TallerSimpleDTO taller)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _TallerDao.RegisterTaller(taller);
                response.Message = "Taller registrado";
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
        /// Actualiza un taller indicado segun su id, se puede agregar una marca o incidar que trabaja con todas
        /// </summary>
        /// <param name="tallerSimpleDTO">taller a actualizar</param>
        /// <returns>taller actualizado</returns>
        [HttpPost("agregar_marca/{tallerId}/{marca}/{agregarTodas?}")]
        public ApplicationResponse<bool> AgregarMarcaATaller([FromRoute] Guid tallerId, [FromRoute] string marca = "-", [FromRoute] bool agregarTodas = false)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _TallerDao.AddMarca(tallerId, marca, agregarTodas);
                response.Message = "marca/s registrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje.ToString();
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}