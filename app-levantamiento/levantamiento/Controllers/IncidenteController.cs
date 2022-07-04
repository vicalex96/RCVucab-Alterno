using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.QueueLogic;
using levantamiento.Persistence.DAOs;
using levantamiento.Exceptions;
using levantamiento.Responses;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que representa el controlador de incidentes, nos da las herramientas para trabajar con el incidente, desde leer la cola de incidentes nuevos hasta mostrar la informacion en detalle
    /// </summary>
    [ApiController]
    [Route("Incidente")]
    public class IncidenteController: Controller
    {
        private readonly IIncidenteDAO _IncidentDAO;
        private readonly ILogger<IncidenteController> _logger;

        public IncidenteController(ILogger<IncidenteController> logger, IIncidenteDAO incidenteDAO)
        {
            _IncidentDAO = incidenteDAO;
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
                IncidenteQueue queue = new IncidenteQueue();
                ICollection<IncidenteQueueDTO> lista = await queue.GetDataFromQueue();
                response.Success = _IncidentDAO.UpdateList(lista);
                response.Message = "listado actualizado, total de elementos: " + lista.Count();
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
        
        /// <summary>
        /// Trae un listado de incidentes sin solicitudes registradas
        /// </summary>
        /// <returns>Incidentes</returns>
        [HttpGet("mostrar_todos/sin_solicitudes")]
        public ApplicationResponse<ICollection<IncidenteToShowDTO>> GetAllWithoutSolicitud()
        {
            var response = new ApplicationResponse<ICollection<IncidenteToShowDTO>>();
            try
            {
                response.Data = _IncidentDAO.GetAllWithoutSolicitud();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "listado de incidentes";
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
        
        /// <summary>
        /// Trae un listado de incidentes con solicitudes registradas
        /// </summary>
        /// <returns>Incidentes</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<ICollection<IncidenteToShowDTO>> GetAll()
        {
            var response = new ApplicationResponse<ICollection<IncidenteToShowDTO>>();
            try
            {
                response.Data = _IncidentDAO.GetAll();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "listado de incidentes";
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


        [HttpGet("mostrar_detalle/{incidenteId}")]
        public async Task<ApplicationResponse<IncidenteDTO>> GetDetaledIncidenteById( [Required] [FromRoute] Guid incidenteId)
        {
            var response = new ApplicationResponse<IncidenteDTO>();
            try
            {
                response.Data = await _IncidentDAO.GetDetailedIncidente(incidenteId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Informacion detallada del incidente";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
        
    }
}