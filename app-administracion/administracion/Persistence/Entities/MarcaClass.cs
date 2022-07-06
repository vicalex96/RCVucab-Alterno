using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  administracion.Persistence.Entities
{
    public class MarcaTaller
    {
        [Key]
        public Guid marcaId { get; set; }
        [Required]
        public Guid tallerId { get; set; }
        [ForeignKey("tallerId")]
        public Taller? taller {get; set;}
        [Required]
        public bool manejaTodas {get; set;}= false;
        public Marca? marca {get; set;} = null;

        public static bool IsMarca (string marca)
        {
            try{
                Marca result = (Marca)Enum.Parse(typeof(Marca), marca);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        public static Marca ConvertToMarca (string marca)
        {
            return (Marca)Enum.Parse(typeof(Marca), marca);
        }
    }
    public class MarcaProveedor
    {
        [Key]
        public Guid marcaId { get; set; }
        [Required]
        public Guid proveedorId { get; set; }
        [ForeignKey("proveedorId")]
        public Proveedor? proveedor {get; set;}
        [Required]
        public bool manejaTodas {get; set;}= false;
        public Marca? marca {get; set;} = null;

        public static bool IsMarca (string marca)
        {
            try{
                Marca result = (Marca)Enum.Parse(typeof(Marca), marca);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        public static Marca ConvertToMarca (string marca)
        {
            return (Marca)Enum.Parse(typeof(Marca), marca);
        }
    }

    public enum Marca
    {
        Toyota,
        Honda,
        Volkswagen,
        Audi,
        BMW,
        Ford,
        Ferrari,
        Hyundai,
        General_Motors,
        Renault,
        Suzuki
        
    }
}
