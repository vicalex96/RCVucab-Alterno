using System;

namespace administracion.Persistence.Entities
{
    public class cotizacion_proveedor
    {
        public Guid cotizacionRepId {get; set;}
        public Guid solicitudRepId {get; set;}
        public float costoManoObra {get; set;} 
        public int estado {get; set;} 
        public DateTime fechaInicioReparacion {get; set;} 
        public DateTime fechaFinReparacion {get; set;} 
    }
}