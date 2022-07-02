
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class AseguradoDTO
    {
        public Guid Id { get; set; }
        public string nombre { get; set; }
        public string apellido {get; set;}
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<VehiculoDTO>? vehiculos {get; set;}
    }
}
