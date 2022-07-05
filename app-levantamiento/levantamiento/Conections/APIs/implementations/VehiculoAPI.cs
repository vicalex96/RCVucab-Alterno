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

        public async Task<bool> RegisterVehiculo(VehiculoRegisterDTO vehiculo)
        {
            var localUrl = url + "vehiculo/crear";
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

                        return APIResponse.Data;
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

                        return vehiculo.Data;
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