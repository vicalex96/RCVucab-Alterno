
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    public class PolizaDTO
    {
        public Guid Id {get; set;}
        public DateTime fechaRegistro {get; set;}
        public DateTime fechaVencimiento {get; set;}
        public string tipoPoliza {get; set;}
        
        public Guid vehiculoId {get; set;}
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public VehiculoDTO vehiculo {get; set;}
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Incidente>? incidente {get; set;}
    }
}
