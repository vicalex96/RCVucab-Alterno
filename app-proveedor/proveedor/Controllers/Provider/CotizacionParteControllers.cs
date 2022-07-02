using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proveedor.BussinesLogic.DTOs;
using proveedor.Persistence.DAOs;
using proveedor.Persistence.DAOs.Implementations;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Exceptions;
using proveedor.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proveedor.Controllers
{
    [ApiController]
    [Route("CotizacionParte")]
    public class CotizacionParteController: Controller
    {
        private readonly ICotizacionParteDAO _cotpatDAO;
        private readonly ILogger<CotizacionParteController> _logger;

        public CotizacionParteController(ILogger<CotizacionParteController> logger, ICotizacionParteDAO cotpatDAO)
        {
            _cotpatDAO = cotpatDAO;
            _logger = logger;
        }

        [HttpPost("Crar CotPat")]
        public ApplicationResponse<string> createCotizacionParte([FromBody] CotizacionParteDTO cotPt)
        {
            var response = new ApplicationResponse<string>();
            try
            {
                response.Data = _cotpatDAO.createCotizacionParte(cotPt);
            }
            catch (ProveedorException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpGet("mostrar_todas_Las_cotizacionesPartes")]
        public ApplicationResponse<List<CotizacionParteDTO>> GetCotizacionPartes()
        {
            var response = new ApplicationResponse<List<CotizacionParteDTO>>();
            try
            {

                response.Data = _cotpatDAO.GetCotizacionPartes();
            }
            catch (ProveedorException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }

        
        [HttpGet("consultarCotpat_por/{estado}")]
        public ApplicationResponse<List<CotizacionParteDTO>> GetCotizacionPartesByestado([FromRoute] EstadoCotPt estadocotpi)
        {
            var response = new ApplicationResponse<List<CotizacionParteDTO>>();
            try
            {
                response.Data = _cotpatDAO.GetCotizacionPartesByestado(estadocotpi);
            }
            catch (ProveedorException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }


        [HttpPatch("actualizar/{estado}")]
        public ApplicationResponse<string> actualizarCotizacionParte([FromRoute] Guid CotizacionParteID, [Required][FromRoute] EstadoCotPt estado)
        {
            var response = new ApplicationResponse<string>();
            try
            {
                response.Data = _cotpatDAO.actualizarCotizacionParte(CotizacionParteID, estado);

            }
            catch (ProveedorException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
    }
}