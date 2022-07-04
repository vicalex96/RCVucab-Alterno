using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace administracion.Persistence.Entities
{
    public class Vehiculo
    {
        [Key]
        public Guid vehiculoId { get; set; }

        public int anioModelo { get; set; }

        public DateTime fechaCompra { get; set; }

        public Color color { get; set; }
        [MaxLength(7)]
        public string? placa { get; set; }
        [Required]
        public Marca marca {get; set;}


        public Guid? aseguradoId {get; set;}
        [ForeignKey("aseguradoId")]
        public Asegurado? asegurado { get; set; } = null;
        public ICollection<Poliza>? polizas {get; set;} = null;
    }

    public enum Color
    {
        Rojo,
        Verde,
        Azul_oscuro,
        Azul_claro,
        Amarillo,
        Morado,
        Naranja,
        Marron,
        Violeta,
        Plateado,
        Dorado,
        Cobre,
        Blanco,
        Azul,
        Negro,

    }
}

    

