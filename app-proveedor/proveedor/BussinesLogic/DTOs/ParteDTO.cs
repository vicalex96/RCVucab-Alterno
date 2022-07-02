using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proveedor.BussinesLogic.DTOs
{
    public class ParteDTO
    {
        public Guid ParteId {get; set;}
        public string Nombre {get; set;}
        public ICollection<RequerimientoDTO>? requerimientos {get; set;}
        
    }
}