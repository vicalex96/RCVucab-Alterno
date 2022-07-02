using System;

namespace proveedor.BussinesLogic.DTOs
{
    public class CotizacionParteDTO
    {
        public Guid CotizacionParteId {get; set;}
        public Guid  ProveedorId {get; set;}
        public float PrecioParteUnidad {get; set;}
        public List<Guid> Requerimientos {get; set;}
        public DateTime FechaEntrega {get; set;}
        public string estado {get; set;}
        public Guid RequerimientoId {get; set;}
        public virtual ProveedorDTO? proveedores { get; set; }
    }
}
