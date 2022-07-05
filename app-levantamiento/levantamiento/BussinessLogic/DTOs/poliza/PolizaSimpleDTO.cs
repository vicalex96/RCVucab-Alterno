
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class PolizaRegisterDTO
    {
        public Guid Id {get; set;}
        public DateTime fechaRegistro {get; set;}
        public DateTime fechaVencimiento {get; set;}
        public string tipoPoliza {get; set;}
        
        public Guid vehiculoId {get; set;}
    }
}
