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
    [Route("Requerimiento")]
    public class RequerimientosController : Controller
    {
        private readonly IRequerimientoDAO _RequerimientoDao;
        private readonly ILogger<RequerimientosController> _logger;

        public RequerimientosController(ILogger<RequerimientosController> logger, IRequerimientoDAO RequerimientoDao)
        {
            _RequerimientoDao = RequerimientoDao;
            _logger = logger;
        }

        [HttpGet("mostrar_por/{solicitudId}")]
        public ApplicationResponse<List<RequerimientoDTO>> ConsultarTalleres([Required][FromRoute] Guid solicitudId)
        {
            var response = new ApplicationResponse<List<RequerimientoDTO>>();
            try
            {
                response.Data = _RequerimientoDao.GetRequerimientos(solicitudId);
                response.Success = true;
                response.Message = "Rquerimientos encontrados";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpPatch("actualizar_tipo/{requerimientoId}/{tipo}")]
        public ApplicationResponse<bool> ActualizarTipoRequerimiento([Required][FromRoute] Guid requerimientoId,[Required][FromRoute] TipoRequerimiento tipo)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _RequerimientoDao.UpdateTipoRequerimiento(requerimientoId,tipo);
                response.Success = true;
                response.Message = "Tipo de requerimiento actualizado";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
        
        [HttpPatch("actualizar_cantidad/{requerimientoId}/{cantidad}")]
        public ApplicationResponse<bool> ActualizarCantidadPiezasRequerimiento([Required][FromRoute] Guid requerimientoId,[Required][FromRoute] int cantidad)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _RequerimientoDao.UpdateQuantityPiecesRequerimiento(requerimientoId,cantidad);
                response.Success = true;
                response.Message = "Cantidad de piezas actualizada";
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
        public ApplicationResponse<bool> RegistrarRequerimiento([Required][FromBody] RequerimientoDTO requerimiento)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _RequerimientoDao.RegisterRequerimiento(requerimiento);
                response.Success = true;
                response.Message = "Requerimiento registrado";
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
