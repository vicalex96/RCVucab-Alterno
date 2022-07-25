using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.BussinesLogic.Logic;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;
using levantamiento.Responses;
using System.ComponentModel.DataAnnotations;

namespace levantamiento.Controllers
{
    /// <summary>
    /// Clase que representa el controlador de incidentes, nos da las herramientas para trabajar con el incidente, desde leer la cola de incidentes nuevos hasta mostrar la informacion en detalle
    /// </summary>
    [ApiController]
    [Route("Incidente")]
    public class IncidenteController: Controller
    {
        private readonly IIncidenteDAO _incidentDAO;
        private readonly IIncidenteLogic _incidentLogic;
        private readonly ILogger<IncidenteController> _logger;

        public IncidenteController(ILogger<IncidenteController> logger, IIncidenteDAO incidenteDAO, IIncidenteLogic incidenteLogic)
        {
            _incidentDAO = incidenteDAO;
            _incidentLogic = incidenteLogic;
            _logger = logger;
        }

        /// <summary>
        /// actualiza el listado de incidentes que esten en la colar
        /// </summary>
        /// <returns>True or False</returns>
        [HttpPost("actualizar_listado")]
        public ApplicationResponse<bool> UpdateListado()
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                int count = _incidentLogic.UpdateIncidenteRegisters();
                response.Success = true;
                response.Message = "listado actualizado, total de elementos: " + count;
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
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
                response.Data = _incidentDAO.GetAllWithoutSolicitud();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "listado de incidentes";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion!.ToString();
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
                response.Data = _incidentDAO.GetAll();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "listado de incidentes";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }


        [HttpGet("mostrar_detalle/{incidenteId}")]
        public async Task<ApplicationResponse<IncidenteDTO>> GetDetaledIncidenteById( [Required] [FromRoute] Guid incidenteId)
        {
            var response = new ApplicationResponse<IncidenteDTO>();
            try
            {
                response.Data = await _incidentLogic.GetDetailedIncidente(incidenteId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Informacion detallada del incidente";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
        
    }
}