using System;
using System.Collections.Generic;

namespace administracion.Persistence.Entities
{
    public class Proveedor
    {
        public Guid proveedorId { get; set; }
        public string nombreLocal {get; set;}
        public ICollection<MarcaProveedor> marcas {get; set;}
    }
}