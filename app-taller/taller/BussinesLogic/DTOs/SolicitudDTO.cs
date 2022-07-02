
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class SolicitudDTO
    {
        public Guid solicitudRepId {get; set;}
        public Guid incidenteId {get; set;}
        public Guid vehiculoId {get; set;}
        public Guid tallerId {get; set;}
    }
}
        
