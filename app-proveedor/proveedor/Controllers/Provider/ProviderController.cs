using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using proveedor.BussinesLogic.DTOs;
using proveedor.Exceptions;
using proveedor.Persistence.DAOs.Interfaces;
using proveedor.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proveedor.Controllers.Provider
{
    [ApiController]
    [Route("provider")]
    public class ProviderController : Controller
    {
        private readonly IProviderDAO _providerDAO;
        private readonly ILogger<ProviderController> _logger;

        public ProviderController(ILogger<ProviderController> logger, IProviderDAO providerDAO)
        {
            _providerDAO = providerDAO;
            _logger = logger;
        }

       /* [HttpGet("providers/{brand}")]
        public ApplicationResponse<List<MarcaDTO>> GetProvidersByBrand([Required][FromRoute] string brand)
        {
            var response = new ApplicationResponse<List<MarcaDTO>>();
            try
            {
                response.Data = _providerDAO.GetProvidersByBrand(brand);
            }
            catch (ProveedorException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }*/
    }
}
