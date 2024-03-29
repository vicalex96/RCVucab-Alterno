
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using  administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO para mostra informacion de un Vehiculo
    /// </summary>
    public class VehiculoDTO
    {
        public Guid Id { get; set; }
        public int anioModelo { get; set; }
        public DateTime fechaCompra { get; set; }
        public string color { get; set; } = "";

        public string placa { get; set; } ="";
        public string marca {get; set;} ="";
        public virtual AseguradoDTO? asegurado { get; set; }
        public virtual ICollection<PolizaDTO>? polizas {get; set;}
    }
}
