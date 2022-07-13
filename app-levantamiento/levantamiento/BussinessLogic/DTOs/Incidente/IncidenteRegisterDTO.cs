using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class IncidenteRegisterDTO
    {
        public Guid incidenteId {get; set;} 
        public Guid polizaId {get; set;}
    }

}