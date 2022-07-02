using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    [ApiController]
    [Route("Proveedor")]
    public class ProveedorController: Controller
    {
        private readonly IProveedorDAO _proveedorDao;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ILogger<ProveedorController> logger, IProveedorDAO proveedorDao)
        {
            _proveedorDao = proveedorDao;
            _logger = logger;
        }
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<ProveedorDTO>> ConsultarProveedores()
        {
            var response = new ApplicationResponse<List<ProveedorDTO>>();
            try
            {
                response.Data = _proveedorDao.GetProveedores();
                response.Success = true;
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }

        [HttpGet("buscar_por/{proveedorId}")]
        public ApplicationResponse<ProveedorDTO> ConsultarProveedorPorId([Required][FromRoute] Guid proveedorId)
        {
            var response = new ApplicationResponse<ProveedorDTO>();
            try
            {
                response.Data = _proveedorDao.GetProveedorByGuid(proveedorId);
                response.Success = true;
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
        public ApplicationResponse<bool> RegistrarProveedor([FromBody] ProveedorSimpleDTO proveedor)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _proveedorDao.RegisterProveedor(proveedor);
                response.Message = "Proveedor Registrado";
            }
            catch (RCVException ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Exception = ex.Excepcion.ToString();
            };
            return response;
        }
        
        [HttpPost("agregar_marca/{ProveedorId}/{marca}/{agregarTodas}")]
        public ApplicationResponse<bool> AgregarMarcaAProveedor([Required][FromRoute] Guid ProveedorId, [FromRoute] string marca = "-", [FromRoute] bool agregarTodas = false)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Success = _proveedorDao.AddMarca(ProveedorId, marca, agregarTodas);
                response.Message = "marca registrada";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Message.ToString();
                response.Exception = ex.Excepcion.ToString();
            }
            return response;
        }
    }
}