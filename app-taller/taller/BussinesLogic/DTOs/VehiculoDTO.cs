
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using taller.Persistence.Entities;

namespace taller.BussinesLogic.DTOs
{
    public class VehiculoDTO
    {
        public Guid Id { get; set; }
        public int anioModelo { get; set; }
        public DateTime fechaCompra { get; set; }
        public string color { get; set; }

        public string placa { get; set; }
        public string marca {get; set;}
        public Guid? aseguradoId {get; set;}

    }
}
        
