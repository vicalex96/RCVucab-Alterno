
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class RequerimientoDTO
    {
        public Guid Id { get; set; }
        public Guid solicitudRepId {get; set;}
        public Guid parteId {get; set;}
        public string descripcion {get; set;}
        public string tipoRequerimiento {get; set;}
        public int cantidad {get; set;}
        public string estado {get; set;}
    }
}
        
