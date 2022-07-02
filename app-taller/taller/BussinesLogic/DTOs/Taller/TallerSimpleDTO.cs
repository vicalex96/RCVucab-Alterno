using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taller.Persistence.Entities;
using System.Text.Json.Serialization;

namespace taller.BussinesLogic.DTOs
{
    public class TallerSimpleDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;} 
    }
}
