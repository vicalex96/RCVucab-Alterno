using Microsoft.AspNetCore.Mvc;
using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.DAOs;
using administracion.Exceptions;
using administracion.Responses;
using administracion.BussinesLogic.Commands;
using System.ComponentModel.DataAnnotations;

namespace administracion.Controllers
{
    /// <summary>
    /// Clase que contiene los endpoints de los vehiculos
    /// </summary>
    [ApiController]
    [Route("vehiculo")]
    public class VehiculoController: Controller
    {
        private readonly ILogger<VehiculoController> _logger;

        public VehiculoController(ILogger<VehiculoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Muestra la lista de vehiculos registrados en el sistema
        /// </summary>
        /// <returns>Lista de vehiculos</returns>
        [HttpGet("mostrar_todos")]
        public ApplicationResponse<List<VehiculoDTO>> GetAllVehiculos()
        {
            var response = new ApplicationResponse<List<VehiculoDTO>>();
            try
            {
                GetAllVehiculosCommand command = VehiculoCommandFactory
                    .createGetAllVehiculosCommand();

                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = " Vehiculos encontrados";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }

        /// <summary>
        /// Muestra la informacion de un vehiculo indicado por el Id
        /// </summary>
        /// <param name="vehiculoId">Id del vehiculo</param>
        /// <returns>Vehiculo</returns>
        [HttpGet("buscar_por/{vehiculoId}")]
        public ApplicationResponse<VehiculoDTO> GetVehiculoByGuid([Required][FromRoute] Guid vehiculoId)
        {
            var response = new ApplicationResponse<VehiculoDTO>();
            try
            {
                GetVehiculosByAseguradoIdCommand command = VehiculoCommandFactory
                    .createGetVehiculosByAseguradoIdCommand(vehiculoId);

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Vehiculo registrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            };
            return response;
        }

        /// <summary>
        /// Crea un nuevo vehiculo en el sistema
        /// </summary>
        /// <param name="vehiculo">Vehiculo a crear</param>
        /// <returns>Vehiculo registrado</returns>
        [HttpPost("registrar")]
        public ApplicationResponse<int> createVehiculo([FromBody] VehiculoRegisterDTO Vehiculo)
        {
            var response = new ApplicationResponse<int>();
            try
            {
                RegisterVehiculoCommand command = VehiculoCommandFactory
                    .createRegisterVehiculoCommand(Vehiculo);

                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Vehiculo registrado";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }

        /// <summary>
        /// Actualiza la informacion de un vehiculo para agregarle asegurado
        /// </summary>
        /// <param name="vehiculoId">Id Vehiculo a actualizar</param>
        /// <param name="aseguradoId">Id asegurado a asociar</param>
        /// <returns>Vehiculo actualizado</returns>
        [HttpPost("asociar_asegurado/{vehiculoId}/{aseguradoId}")]
        public ApplicationResponse<int> AddAsegurado([Required][FromRoute] Guid vehiculoId ,[Required][FromRoute] Guid aseguradoId)
        {
            var response = new ApplicationResponse<int>();
            try
            {
                AddAseguradoToVehiculoCommand command = VehiculoCommandFactory
                    .createAddAseguradoToVehiculoCommand(vehiculoId, aseguradoId);

                command.Execute();
                response.Data = command.GetResult();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Success = true;
                response.Message = "Vehiculo asociado con Asegurado correctamente";
            }
            catch (RCVException ex)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.Success = false;
                response.Message = ex.Mensaje;
                response.Exception = ex.Excepcion!.ToString();
            }
            return response;
        }
    }
}