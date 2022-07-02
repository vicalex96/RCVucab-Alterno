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
    [Route("Taller")]
    public class TallerController : Controller
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
