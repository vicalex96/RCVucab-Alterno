using Microsoft.AspNetCore.Mvc;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.DataAccess.DAOs;
using levantamiento.Exceptions;
using levantamiento.Responses;
using levantamiento.BussinesLogic.Logic;

namespace levantamiento.Controllers
{
    [ApiController]
    [Route("Requerimiento")]
    public class RequerimientoController: Controller
    {
        private readonly IRequerimientoDAO _requerimientoDAO;
        private readonly IRequerimientoLogic _requerimientoLogic;
        private readonly ILogger<RequerimientoController> _logger;

        public RequerimientoController(ILogger<RequerimientoController> logger, IRequerimientoDAO requerimientoDAO, IRequerimientoLogic requerimientoLogic)
        {
            _logger = logger;
            _requerimientoLogic = requerimientoLogic;
            _requerimientoDAO = requerimientoDAO;         
        }

        ///<summary>
        ///Obtiene todos los requerimientos de la una solicitud
        ///</summary>
        ///<param name="SolicitudId">Solicitud de la cual se desean obtener los requerimientos</param>
        ///<returns>Respuesta con la lista de requerimientos</returns>
        [HttpGet("obtener_por/{SolicitudId}")]
        public ApplicationResponse<List<RequerimientoDTO>> GetRequerimientosBySolicitudId(Guid SolicitudId)
        {
            var response = new ApplicationResponse<List<RequerimientoDTO>>();
            try
            {
                response.Data = _requerimientoDAO.GetRequerimientosBySolicitudId(SolicitudId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Requerimientos obtenidos";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Message = ex.Mensaje;
                response.Success = false;
            }
            return response;
        }

        //[HttpGet("buscar_por/{SolicitudId}")]
        ///<summary>
        ///Registra un requerimiento para la solicitud indicada
        ///</summary>
        ///<param name="RequerimientoDTO">requerimiento que se va a registrar</param>
        ///<returns>Respuesta bool true or false indicando el exito de la operacion</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegisterRequerimiento ([FromBody] RequerimientoRegisterDTO requerimiento)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _requerimientoLogic.RegisterRequerimiento(requerimiento);
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
        
    }
}