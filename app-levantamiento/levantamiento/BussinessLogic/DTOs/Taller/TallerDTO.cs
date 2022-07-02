using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using levantamiento.Persistence.Entities;
using System.Text.Json.Serialization;

namespace levantamiento.BussinesLogic.DTOs
{
    public class TallerDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;}
        public ICollection<MarcaDTO>? marcas {get; set;} 
    }
}
