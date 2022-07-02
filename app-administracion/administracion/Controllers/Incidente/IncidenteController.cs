using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    [ApiController]
    [Route("Incidente")]
    public class IncidenteController: Controller
    {
        private readonly IIncidenteDAO _incidenteDao;
        private readonly ILogger<IncidenteController> _logger;

        public IncidenteController(ILogger<IncidenteController> logger, IIncidenteDAO incidenteDao)
        {
            _incidenteDao = incidenteDao;
            _logger = logger;
        }

        [HttpGet("consultar/{incidenteID}")]
        public ApplicationResponse<IncidenteDTO> consultarIncidente([Required][FromRoute] Guid incidenteID)
        {
            var response = new ApplicationResponse<IncidenteDTO>();
            try
            {
                response.Data = _incidenteDao.consultarIncidente(incidenteID);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpGet("consultar_lista_activos")]
        public ApplicationResponse<List<IncidenteDTO>> ConsultarIncidentesActivos()
        {
            var response = new ApplicationResponse<List<IncidenteDTO>>();
            try
            {
                response.Data = _incidenteDao.ConsultarIncidentesActivos();
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
        public ApplicationResponse<bool> RegistrarIncidente([FromBody] IncidenteSimpleDTO incidente)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _incidenteDao.RegisterIncidente(incidente);
                response.Message = "Incidente registrado";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpPatch("actualizar/{incidenteID}/{estado}")]
        public ApplicationResponse<bool> actualizarIncidente([Required][FromRoute] Guid incidenteID, [Required][FromRoute] EstadoIncidente estado)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _incidenteDao.actualizarIncidente(incidenteID, estado);

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