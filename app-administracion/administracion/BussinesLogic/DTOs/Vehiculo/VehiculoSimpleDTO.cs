
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;

namespace administracion.BussinesLogic.DTOs
{
    public class VehiculoSimpleDTO
    {
        public Guid Id { get; set; }
        public int anioModelo { get; set; }
        public DateTime fechaCompra { get; set; }
        public string color { get; set; }
        public string placa { get; set; }
        public string marca {get; set;}
    }
}
