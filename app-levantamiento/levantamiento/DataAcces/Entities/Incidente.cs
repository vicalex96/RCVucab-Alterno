using System.ComponentModel.DataAnnotations;

namespace levantamiento.DataAccess.Entities
{
    public class Incidente
    { 
        [Key]
        public Guid Id {get; set;}
        public Guid polizaId {get; set;}
        public ICollection<SolicitudReparacion>? solicitudes {get; set;}

        public Incidente(){
            Id = Guid.NewGuid();
        }
    }
}
