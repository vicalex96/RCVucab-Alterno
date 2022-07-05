using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO para mostrar informacion de incidente
    /// </summary>
    public class IncidenteDTO
    {
        public Guid Id {get; set;} 
        public Guid polizaId {get; set;}
        public PolizaDTO? poliza {get; set;}
        public string estadoIncidente {get; set;} = "";
    }

}