using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace levantamiento.Persistence.Entities
{
    public class SolicitudReparacion
    {
        [Key]
        public Guid SolicitudReparacionId {get; set;}
        
        [Required]
        public Guid incidenteId {get; set;}

        [Required]
        public Guid vehiculoId {get; set;}
        public Guid tallerId {get; set;}
        public DateTime fechaSolicitud {get; set;}

        [ForeignKey("incidenteId")]
        public virtual Incidente? incidente {get; set;}
        public virtual ICollection<Requerimiento>? requerimientos {get; set;}
    }
}