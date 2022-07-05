using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using levantamiento.Persistence.Entities;

namespace levantamiento.BussinesLogic.DTOs
{
    public class TallerRegisterDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;} 
    }
}
