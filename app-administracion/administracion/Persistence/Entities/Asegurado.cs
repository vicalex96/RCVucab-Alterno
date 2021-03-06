using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Asegurado
    {
        [Key]
        public Guid Id {get; set;}
        [Required][MaxLength(100)]
        public string nombre {get; set;} ="";
        [Required][MaxLength(100)]
        public string apellido {get; set;} ="";
        public virtual ICollection<Vehiculo>? vehiculos {get; set;}
    }
}