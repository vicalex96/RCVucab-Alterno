using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;
using administracion.Conections.rabbit;
using administracion.BussinesLogic.LogicClasses;

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
        private readonly ITallerLogic _tallerLogic;

        private readonly ILogger<TallerController> _logger;

        public TallerController(ILogger<TallerController> logger, ITallerDAO TallerDao, ITallerLogic tallerLogic)
        {
            _TallerDao = TallerDao;
            _logger = logger;
            _tallerLogic = tallerLogic;
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
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Listado de talleres cargado";
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
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "taller encontrado";
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
        /// Registra un taller en el sistema
        /// </summary>
        /// <param name="tallerRegisterDTO">Taller a registrar</param>
        /// <returns>Taller registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarTaller([FromBody] TallerRegisterDTO taller)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _tallerLogic.RegisterTaller(taller);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Taller registrado";
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
        /// Actualiza un taller indicado segun su id, se puede agregar una marca o incidar que trabaja con todas
        /// </summary>
        /// <param name="tallerRegisterDTO">taller a actualizar</param>
        /// <returns>taller actualizado</returns>
        [HttpPatch("agregar_marca/{tallerId}/{marca}")]
        public ApplicationResponse<bool> AgregarMarcaATaller([FromRoute] Guid tallerId, [FromRoute] string marca)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _tallerLogic.AddMarca(tallerId, marca);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Especializacion agregada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje.ToString();
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
        
        /// <summary>
        /// Especializa el taller en todas las marcas
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <returns>Boleano true si todo salio bien</returns>
        [HttpPatch("agrega_marca/todas/{tallerId}")]
        public ApplicationResponse<bool> AgregarTodasLasMarcasATaller([FromRoute] Guid tallerId)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _tallerLogic.AddAllMarcas(tallerId);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Especializaciones agregadas";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje.ToString();
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
    }
}