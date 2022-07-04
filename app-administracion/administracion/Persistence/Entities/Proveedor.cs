
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Proveedor
    {
        [Key]
        public Guid proveedorId { get; set; }
        [Required]
        public string nombreLocal {get; set;} ="";
        public ICollection<MarcaProveedor>? marcas {get; set;}
    }
}