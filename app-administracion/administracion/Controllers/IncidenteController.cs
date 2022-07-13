using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;
using administracion.BussinesLogic.LogicClasses;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que representa el controlador de incidentes,permite mostrar los incidentes, registrarlos y actualizalos
    /// </summary>
    [ApiController]
    [Route("Incidente")]
    public class IncidenteController: Controller
    {
        private readonly IIncidenteDAO _incidenteDao;

        private readonly IIncidenteLogic _incidenteLogic;
        private readonly ILogger<IncidenteController> _logger;

        public IncidenteController(ILogger<IncidenteController> logger, IIncidenteDAO incidenteDao, IIncidenteLogic incidenteLogic)
        {
            _incidenteDao = incidenteDao;
            _incidenteLogic = incidenteLogic;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista de incidentes registrados en el sistema
        /// </summary>
        /// <returns>Lista de incidentes</returns>
        [HttpGet("consultar/{incidenteID}")]
        public ApplicationResponse<IncidenteDTO> consultarIncidente([Required][FromRoute] Guid incidenteID)
        {
            var response = new ApplicationResponse<IncidenteDTO>();
            try
            {
                response.Data = _incidenteDao.GetIncidenteById(incidenteID);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Obtiene una lista de incidentes registrados en el sistema
        /// </summary>
        /// <returns>Lista de incidentes</returns>
        [HttpGet("consultar/estado/{estado}")]
        public ApplicationResponse<List<IncidenteDTO>> ConsultarIncidentesPorEstado([Required][FromRoute] EstadoIncidente estado)
        {
            var response = new ApplicationResponse<List<IncidenteDTO>>();
            try
            {
                response.Data = _incidenteDao.GetIncidentesByState(estado);
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Registra un incidente en el sistema
        /// </summary>
        /// <param name="incidenteRegisterDTO">Incidente a registrar</param>
        /// <returns>Incidente registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarIncidente([FromBody] IncidenteRegisterDTO incidente)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _incidenteLogic.RegisterIncidente(incidente);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Incidente registrado";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Actualiza el estadod el incidente
        /// </summary>
        /// <param name="incidenteId">Incidente a actualizar</param>
        /// <param name="estado">el estado del incidente</param>
        /// <returns>Incidente actualizado</returns>
        [HttpPatch("actualizar/{incidenteID}/{estado}")]
        public ApplicationResponse<bool> UpdateIncidente([Required][FromRoute] Guid incidenteID, [Required][FromRoute] EstadoIncidente estado)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _incidenteLogic.UpdateIncidenteState(incidenteID, estado);

            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }
    }
}