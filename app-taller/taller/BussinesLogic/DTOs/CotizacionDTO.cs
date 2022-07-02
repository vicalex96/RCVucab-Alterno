
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class CotizacionRepDTO
    {  
        public Guid Id {get; set;}
        public Guid tallerId {get; set;}
        public float costoManoObra {get; set;}
        public string estado {get; set;}
        public DateTime fechaInicioReparacion {get; set;}
        public DateTime fechaFinReparacion { get; set;}
        public Guid solicitudRepId {get; set;}
    }
}
        
