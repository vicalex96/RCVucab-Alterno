using Microsoft.AspNetCore.Mvc;
using taller.Conections.APIs;
using taller.BussinesLogic.DTOs;
using taller.Persistence.Entities;
using taller.BussinesLogic.QueueLogic;
using taller.Persistence.DAOs;
using taller.Responses;
using taller.Exceptions;
using System.ComponentModel.DataAnnotations;


namespace taller.Controllers
{
    [ApiController]
    [Route("SolicitudReparacion")]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudDAO _SolicitudDAO;
        private readonly ILogger<SolicitudController> _logger;

        public SolicitudController(ILogger<SolicitudController> logger, ISolicitudDAO solicitudDAO)
        {
            _SolicitudDAO = solicitudDAO;
            _logger = logger;
        }

        /// <summary>
        /// actualiza el listado de incidentes que esten en la colar
        /// </summary>
        /// <returns>True or False</returns>
        [HttpGet("actualizar_listado")]
        public async Task<ApplicationResponse<bool>> UpdateListado()
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                SolicitudQueue queue = new SolicitudQueue();
                ICollection<SolicitudQueueDTO> lista = await queue.GetDataFromQueue();
                response.Success = _SolicitudDAO.UpdateList(lista);
                response.Message = "listado actualizado, total de elementos: " + lista.Count();
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
        
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<SolicitudDTO>> ConsultarSolicitudes()
        {
            var response = new ApplicationResponse<List<SolicitudDTO>>();
            try
            {
                response.Data = _SolicitudDAO.GetSolicitudes();
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
