using System;
using System.Collections.Generic;

namespace administracion.Persistence.Entities
{
    public class cotizacion_taller
    {
        public Guid cotizacionParteId {get; set;}
        public Guid proveedorId {get; set;}
        public float percioParteUnidad  {get; set;}
        public List<Guid> requerimientos {get; set;}
        public DateTime fechaEntrega {get; set;}
        public int estado {get; set;}

    }
}