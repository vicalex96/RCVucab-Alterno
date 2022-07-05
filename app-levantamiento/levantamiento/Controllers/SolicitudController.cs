using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Persistence.DAOs;
using levantamiento.Exceptions;
using levantamiento.Responses;
using System.ComponentModel.DataAnnotations;
using levantamiento.Conections.rabbit;

namespace administracion.Controllers
{
    [ApiController]
    [Route("Solicitud")]
    public class SolicitudController: Controller
    {
        private readonly ILogger<SolicitudController> _logger;
        private readonly ISolicitudReparacionDAO _SolicitudDAO;

        public SolicitudController(ILogger<SolicitudController> logger,
        ISolicitudReparacionDAO solicitudReparacionDAO)
        {
            _SolicitudDAO = solicitudReparacionDAO;
            _logger = logger;
        }

        /// <summary>
        /// Muestra el listado de todas las solicitudes en el sistema
        /// </summary>
        /// <returns>Solicitudes</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<SolicitudesResparacionDTO>> GetAll()
        {
            var response = new ApplicationResponse<List<SolicitudesResparacionDTO>>();
            try
            {
                response.Data = _SolicitudDAO.GetAll();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "solicitudes encontradas";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

                /// <summary>
        /// Busca las solicitudes que aun no tengan un taller asociado
        /// </summary>
        /// <returns>Solicitudes</returns>
        [HttpGet("mostrar_todos/sin_taller")]
        public ApplicationResponse<List<SolicitudesResparacionDTO>> GetSolicitudesSinTaller()
        {
            var response = new ApplicationResponse<List<SolicitudesResparacionDTO>>();
            try
            {
                response.Data = _SolicitudDAO.GetSolicitudWithoutTaller();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "solicitudes encontradas";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Busca una solicitud por su id
        /// </summary>
        /// <param name="SolicitudId">Id de la solicitud</param>
        /// <returns>Solicitud</returns>
        [HttpGet("buscar_por/solicitud/{solicitudId}")]
        public ApplicationResponse<SolicitudesResparacionDTO> GetSolicitud(Guid solicitudId)
        {
            var response = new ApplicationResponse<SolicitudesResparacionDTO>();
            try
            {
                response.Data = _SolicitudDAO.GetSolicitudById(solicitudId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "solicitud encontrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Busca las solicitudes segun el id del incidente
        /// </summary>
        /// <param name="IncidenteId">Id del incidente</param>
        /// <returns>Solicitudes</returns>
        [HttpGet("buscar_por/incidente/{IncidenteId}")]
        public ApplicationResponse<List<SolicitudesResparacionDTO>> GetSolicitudesByIncidente(Guid IncidenteId)
        {
            var response = new ApplicationResponse<List<SolicitudesResparacionDTO>>();
            try
            {
                response.Data = _SolicitudDAO.GetSolicitudByIncidenteId(IncidenteId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "solicitudes encontradas";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Registrar los datos basicos de un incidente
        /// </summary>
        /// <param name="solicitud">Solicitud</param>
        /// <returns>bool</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarSolicitud([Required][FromBody] SolicitudesRespacionRegisterDTO solicitud)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _SolicitudDAO.RegisterSolicitud(solicitud);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "solicitud registrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        /// <summary>
        /// Recarga la cola de solicitudes para el taller en caso de que falten datos
        /// </summary>
        /// <returns>Solicitudes</returns>
        [HttpGet("cargar_cola")]
        public ApplicationResponse<bool> RefrescarCola()
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _SolicitudDAO.SendNotificationsToQueue();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "cola actualizada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}