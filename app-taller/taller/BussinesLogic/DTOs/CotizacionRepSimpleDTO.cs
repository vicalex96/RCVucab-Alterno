
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class CotizacionRepRegisterDTO
    {  
        public Guid Id {get; set;}
        public Guid tallerId {get; set;}
        public float costoManoObra {get; set;}
        public Guid solicitudRepId {get; set;}
    }
}
        
