using System.Text.Json;
using System.Net.Http;
using System.Text;
using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;
using taller.Persistence.DAOs;
using taller.Responses;
using taller.Exceptions;

namespace taller.Conections.APIs
{
    public class ServicioAPI //: IServicioAPI
    {
        
        
        private string url = "https://localhost:7281/";
        private JsonSerializerOptions options = new JsonSerializerOptions() 
        {
            PropertyNameCaseInsensitive = true
        };

        //implement IServicioAPI methods
        public async Task<ApplicationResponse<TallerDTO>> GetTaller(Guid tallerId)
        {
            var localUrl = url + "Taller/buscar_por/{0}";
            try
            {
                using(var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(
                        string.Format(localUrl, tallerId.ToString())
                    );

                    if(response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var taller = JsonSerializer.Deserialize<ApplicationResponse<TallerDTO>>(content, options);

                        Console.WriteLine("Talleres: {0} -> {1}",taller.Data.nombreLocal, taller.Data.marcas.ToList()[0].nombreMarca);

                        TallerRegisterDTO t = new TallerRegisterDTO()
                        {
                            Id = taller.Data.Id,
                            nombreLocal = taller.Data.nombreLocal,
                        };
                        return taller;
                    }    
                    else
                        Console.WriteLine("Error");

                    Console.ReadKey();
                    return null;
                }
            } catch(Exception ex){
                throw new RCVException("ocurrio un error al intar leer la data del taller", ex);
            }
        }
    }
}