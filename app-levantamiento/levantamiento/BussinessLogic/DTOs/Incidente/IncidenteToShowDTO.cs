using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class IncidenteToShowDTO
    {
        public Guid Id {get; set;}
        public ICollection<SolicitudesReparacionDTO>? solicitudesRepacion {get; set;} 
        
    }

}