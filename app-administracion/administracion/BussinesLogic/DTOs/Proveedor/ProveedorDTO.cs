using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    public class ProveedorDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;}
        public ICollection<MarcaDTO>? marcas {get; set;} 
    }
}
