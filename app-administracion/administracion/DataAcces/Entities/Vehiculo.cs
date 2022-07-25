using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using  administracion.DataAccess.Enums;

namespace  administracion.DataAccess.Entities
{
    public class Vehiculo
    {
        [Key]
        public Guid Id { get; set; }

        public int anioModelo { get; set; }

        public DateTime fechaCompra { get; set; }

        public Color color { get; set; }
        [MaxLength(7)]
        public string? placa { get; set; }
        [Required]
        public MarcaName marca {get; set;}


        public Guid? aseguradoId {get; set;}
        [ForeignKey("aseguradoId")]
        public Asegurado? asegurado { get; set; } = null;
        public ICollection<Poliza>? polizas {get; set;} = null;

        public Vehiculo ()
        {
            Id = Guid.NewGuid();
        }
    }


}

    

