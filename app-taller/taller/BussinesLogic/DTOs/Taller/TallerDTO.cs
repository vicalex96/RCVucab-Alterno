using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taller.Persistence.Entities;
using System.Text.Json.Serialization;

namespace taller.BussinesLogic.DTOs
{
    public class TallerDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;}
        public ICollection<MarcaDTO>? marcas {get; set;} 
        public ICollection<SolicitudDTO>? solicitudes {get; set;}
    }
}
