using System;

namespace proveedor.Persistence.Entities
{
    public class CotizacionParteEntity : BaseEntity
    {
     

        public Guid CotizacionParteId {get; set;}
        public Guid  ProveedorId {get; set;}
        public float PrecioParteUnidad {get; set;}
        public List<Guid>? Requerimientos {get; set;}
        public DateTime FechaEntrega {get; set;}
        public EstadoCotPt  estado {get; set;}
        public Guid RequerimientoId {get; set;}
        public virtual List<Requerimiento>? requerimientos {get; set;}
      
}
}

public enum EstadoCotPt
{
    Pendiente,
    Declinado,
    Cotizado,
    OrdenCompra,
    Facturado

}