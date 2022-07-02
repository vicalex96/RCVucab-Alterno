using System;
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Incidente
    {
        public Guid incidenteId {get; set;} 
        [Required]
        public Guid polizaId {get; set;}
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
    Analizando,
    ConSolictud,
    EnReparacion,
    cerrado

}