using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using  administracion.DataAccess.Enums;

namespace  administracion.DataAccess.Entities
{
    public class Poliza
    {
        [Key]
        public Guid Id {get; set;}
        [Required]
        public DateTime fechaRegistro {get; set;}
        [Required]
        public DateTime fechaVencimiento {get; set;}
        [Required]
        public TipoPoliza tipoPoliza {get; set;}
        [Required]
        public Guid vehiculoId {get; set;}
        
        [ForeignKey("vehiculoId")]
        public Vehiculo? vehiculo {get; set;}
        public ICollection<Incidente>? incidente {get; set;}

        public Poliza ()
        {
            Id = Guid.NewGuid();
        }
    }

}