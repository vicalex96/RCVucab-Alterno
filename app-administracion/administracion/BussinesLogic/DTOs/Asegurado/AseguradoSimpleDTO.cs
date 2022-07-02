
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    public class AseguradoSimpleDTO
    {
        public Guid Id { get; set; }
        public string nombre { get; set; }
        public string apellido {get; set;}
    }
}