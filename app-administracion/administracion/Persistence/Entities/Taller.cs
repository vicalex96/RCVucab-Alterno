using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Taller
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string nombreLocal {get; set;} ="";
        public ICollection<MarcaTaller>? marcas {get; set;}

    }
}