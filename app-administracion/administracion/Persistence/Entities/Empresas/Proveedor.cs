
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Proveedor:EmpresaBase
    {
        public ICollection<MarcaProveedor>? marcas {get; set;}
    }
}