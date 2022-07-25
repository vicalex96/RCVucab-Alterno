using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using  administracion.DataAccess.Enums;

namespace  administracion.DataAccess.Entities
{
    public class Incidente
    {
        [Key]
        public Guid Id {get; set;} 
        [Required]
        public Guid polizaId {get; set;}
        [ForeignKey("polizaId")]
        public Poliza? poliza {get; set;}
        [Required]
        public EstadoIncidente estadoIncidente {get; set;} 
        [Required]
        public DateTime fechaRegistrado {get; set;}
        public DateTime? fechaFinalizado {get; set;}

        public Incidente ()
        {
            Id = Guid.NewGuid();
        }
    }
}
