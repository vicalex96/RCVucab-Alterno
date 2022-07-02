using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Poliza
    {
        public Guid polizaId {get; set;}
        [Required]
        public DateTime fechaRegistro {get; set;}
        [Required]
        public DateTime fechaVencimiento {get; set;}
        [Required]
        public TipoPoliza tipoPoliza {get; set;}
        [Required]
        public Guid vehiculoId {get; set;}
        public Vehiculo vehiculo {get; set;}
        public ICollection<Incidente>? incidente {get; set;}
    }

    public enum TipoPoliza {
        CoberturaCompleta,
        DaniosATerceros
    }
}