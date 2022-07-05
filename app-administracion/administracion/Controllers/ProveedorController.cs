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
    /// Clase que contiene los endpoints el trabajo con proveedores
    /// </summary>
    [ApiController]
    [Route("Proveedor")]
    public class ProveedorController: Controller
    {
        private readonly IProveedorDAO _proveedorDao;
        private readonly IProveedorLogic _proveedorLogic;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ILogger<ProveedorController> logger, IProveedorDAO proveedorDao, IProveedorLogic proveedorLogic)
        {
            _proveedorDao = proveedorDao;
            _logger = logger;
            _proveedorLogic = proveedorLogic;
        }
        /// <summary>
        /// Mostrar un listado de proveedores que existen en el sistema
        /// </summary>
        /// <returns>Proveedores en sistema</returns>
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
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Busca un proveedor en el sistema según el Id del proveedor
        /// </summary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <returns>Proveedor</returns>
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
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Registra un proveedor en el sistema
        /// </summary>
        /// <param name="proveedorRegisterDTO">Proveedor a registrar</param>
        /// <returns>Proveedor registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<bool> RegistrarProveedor([FromBody] ProveedorRegisterDTO proveedor)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _proveedorLogic.RegisterProveedor(proveedor);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Proveedor registrado";
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
        /// Actualiza un proveedor indicado segun su id, se puede agregar una marca o incidar que trabaja con todas
        /// </summary>
        /// <param name="proveedorRegisterDTO">Proveedor a actualizar</param>
        /// <returns>Proveedor actualizado</returns>
        [HttpPatch("agregar_marca/{proveedorId}/{marca}")]
        public ApplicationResponse<bool> AgregarMarcaAproveedor([FromRoute] Guid proveedorId, [FromRoute] string marca)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _proveedorLogic.AddMarca(proveedorId, marca);
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
        /// Especializa el proveedor en todas las marcas
        /// </summary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <returns>Boleano true si todo salio bien</returns>
        [HttpPatch("agrega_marca/todas/{proveedorId}")]
        public ApplicationResponse<bool> AgregarTodasLasMarcasAProveedor([FromRoute] Guid proveedorId)
        {
            var response = new ApplicationResponse<bool>();
            try
            {
                response.Data = _proveedorLogic.AddAllMarcas(proveedorId);
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