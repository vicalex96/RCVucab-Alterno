using System.ComponentModel.DataAnnotations;

namespace levantamiento.Persistence.Entities
{
    public class Parte
    {
        [Key]
        public Guid parteId {get; set;}
        [Required]
        public string nombre {get; set;}
    }
}

public enum NombrePartes
{
    Puerta,
    Ventana,
    Puerta_maleta,
    Puerta_capo,
    Rin,
    Neumatico,
    Parabrisas,
    Retrovisores,
    Motor,
    Volante,
    Asiento,
    Faros_delanteros,
    Faros_traseros,
    Caja_de_cambios,

    Empacadura,
    Freno_de_mano,
    pistones,
    Bujias,
}