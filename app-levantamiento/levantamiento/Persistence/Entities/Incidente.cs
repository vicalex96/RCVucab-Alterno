using System.ComponentModel.DataAnnotations;

namespace levantamiento.Persistence.Entities
{
    public class Incidente
    {
        [Key]
        public Guid incidenteId {get; set;}
        public Guid polizaId {get; set;}
        public ICollection<SolicitudReparacion>? solicitudes {get; set;}
    }
}
