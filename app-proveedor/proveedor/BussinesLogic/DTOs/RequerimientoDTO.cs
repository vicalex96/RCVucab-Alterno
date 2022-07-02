using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proveedor.BussinesLogic.DTOs
{
    public class RequerimientoDTO
    {
        public Guid RequerimientoId {get; set;}
        public Guid  SolicitudId {get; set;}
        public Guid ParteId {get; set;}
        public string descripcion {get; set;}
        public int tipoRequerimientos {get; set;}
        public int Cantidad {get; set;}
        public int  estado {get; set;}
        private IList<CotizacionParteDTO>? cotizaciops {get; set;}
        public virtual ParteDTO? partes { get; set; }
        
    }
}
