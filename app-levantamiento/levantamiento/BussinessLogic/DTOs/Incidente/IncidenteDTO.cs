using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class IncidenteDTO
    {
        public Guid Id {get; set;} 
        public Guid polizaId {get; set;}
        public string estadoIncidente {get; set;} 

        public PolizaDTO poliza {get; set;}
        public ICollection<SolicitudesResparacionDTO> solicitudes {get; set;}
    }

}