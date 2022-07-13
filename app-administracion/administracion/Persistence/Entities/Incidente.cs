using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace administracion.Persistence.Entities
{
    public class Incidente
    {
        [Key]
        public Guid incidenteId {get; set;} 
        [Required]
        public Guid polizaId {get; set;}
        [ForeignKey("polizaId")]
        public Poliza? poliza {get; set;}
        [Required]
        public EstadoIncidente estadoIncidente {get; set;} 
        [Required]
        public DateTime fechaRegistrado {get; set;}
        public DateTime? fechaFinalizado {get; set;}
    }
}


public enum EstadoIncidente
{
    Pendiente,
    Procesando,
    Cerrado

}