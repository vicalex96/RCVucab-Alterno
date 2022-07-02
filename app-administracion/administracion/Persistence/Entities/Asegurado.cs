
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



namespace administracion.Persistence.Entities
{
    public class Asegurado
    {
        public Guid aseguradoId {get; set;}
        [Required][MaxLength(100)]
        public string nombre {get; set;}
        [Required][MaxLength(100)]
        public string apellido {get; set;}
        public virtual ICollection<Vehiculo>? vehiculos {get; set;}
    }
}