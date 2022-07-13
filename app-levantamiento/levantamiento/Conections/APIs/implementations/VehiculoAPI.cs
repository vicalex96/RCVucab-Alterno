using System.Text.Json;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Responses;
using levantamiento.Exceptions;

namespace levantamiento.Conections.APIs
{
    public class VehiculoAPI : IVehiculoAPI
    {
        private string url = "https://localhost:7281/";
        private JsonSerializerOptions options = new JsonSerializerOptions() 
        {
            PropertyNameCaseInsensitive = true
        };

        ///<summary>
        ///Metodo asincrono que se comunica con la API de Administracion para registrar un vehiculo
        ///</summary>
        ///<param name="vehiculo">vehiculo que se va a registrar</param>
        ///<returns>Respuesta bool true si el registro fue exitoso</returns>
        public async Task<bool> RegisterVehiculo(VehiculoRegisterDTO vehiculo)
        {
            var localUrl = url + "vehiculo/registrar";
            try
            {
                using(var httpClient = new HttpClient())
                {
                    var serializedVechiulo = JsonSerializer.    Serialize<VehiculoRegisterDTO>(vehiculo);
                    Console.WriteLine(serializedVechiulo);
                    var response = await httpClient.PostAsJsonAsync(localUrl, vehiculo);

                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var APIResponse = JsonSerializer.Deserialize<ApplicationResponse<bool>>(content, options);

                        return APIResponse!.Data;
                    }    
                    else
                        throw new HttpRequestException("ocurrio algun problema al conectarse con el API");
                }
            } 
            catch(HttpRequestException ex)
            {
                throw new RCVException(ex.Message, ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un error desconcido", ex);
            }
        }
        
        ///<summary>
        /// Metodo asincrono que solicita al API de Administracion los datos de un vehiculo segun su Id
        ///</summary>
        ///<param name="vehiculoId">Id del vehiculo que se desea obtener</param>
        ///<returns>Respuesta con el vehiculo solicitado</returns>
        public async Task<VehiculoDTO> GetVehiculoFromAdmin(Guid vehiculoId)
        {
            var localUrl = url + "vehiculo/buscar_por/{0}";
            try
            {
                using(var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(
                        string.Format(localUrl, vehiculoId.ToString())
                    );

                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var vehiculo = JsonSerializer.Deserialize<ApplicationResponse<VehiculoDTO>>(content, options);

                        return vehiculo!.Data!;
                    }    
                    else
                        throw new HttpRequestException("ocurrio algun problema al conectarse con el API");
                }
            } 
            catch(HttpRequestException ex)
            {
                throw new RCVException(ex.Message, ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un error desconcido", ex);
            }
        }
    }

}