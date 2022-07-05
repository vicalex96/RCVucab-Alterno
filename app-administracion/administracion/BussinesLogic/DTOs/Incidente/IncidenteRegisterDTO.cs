using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO para registrar incidente
    /// </summary>
    public class IncidenteRegisterDTO
    {
        public Guid Id {get; set;} 
        public Guid polizaId {get; set;}
    }

}