using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    public class IncidenteSimpleDTO
    {
        public Guid incidenteId {get; set;} 
        public Guid polizaId {get; set;}
    }

}