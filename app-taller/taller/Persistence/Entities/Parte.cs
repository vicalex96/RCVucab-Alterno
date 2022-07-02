namespace taller.Persistence.Entities
{
    public class Parte
    {
        public Guid parteId {get; set;}
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
    Luces_delanteras,
    Luces_traseras,
    Caja_de_cambios,
}