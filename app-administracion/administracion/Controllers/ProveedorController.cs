using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using System.ComponentModel.DataAnnotations;
using administracion.Conections.rabbit;

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
        private readonly IProductorRabbit _ProductorRabbit;
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ILogger<ProveedorController> logger, IProveedorDAO proveedorDao, IProductorRabbit productorRabbit)
        {
            _proveedorDao = proveedorDao;
            _logger = logger;
            _ProductorRabbit = productorRabbit;
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
        /// <param name="proveedorSimpleDTO">Proveedor a registrar</param>
        /// <returns>Proveedor registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<Guid> RegistrarProveedor([FromBody] ProveedorSimpleDTO proveedor)
        {
            var response = new ApplicationResponse<Guid>();
            try
            {
                response.Data = _proveedorDao.RegisterProveedor(proveedor);
                _ProductorRabbit.SendMessage(
                    Routings.proveedor,
                    "registrar_proveedor",
                    response.Data.ToString()
                );
                response.Success = true;
                response.Message = "Proveedor Registrado";
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
        /// <param name="proveedorSimpleDTO">Proveedor a actualizar</param>
        /// <returns>Proveedor actualizado</returns>
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
                response.Message = ex.Mensaje.ToString();
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
    }
}