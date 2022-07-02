
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class ParteDTO
    {
        public Guid Id {get; set;}
        public string nombre {get; set;}
        public string descripcion {get; set;}

    }
}
        
