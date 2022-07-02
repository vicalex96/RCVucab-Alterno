using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

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
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
        
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
                response.Message = ex.Message.ToString();
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}