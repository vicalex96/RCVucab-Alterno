namespace proveedor.Persistence.Entities
{
    public class Requerimiento
    {
        public Guid requerimientoId {get; set;}
        public Guid solicitudRepId {get; set;}
        public Guid parteId {get; set;}
        public string descripcion {get; set;}
        public TipoRequerimiento tipoRequerimiento {get; set;}
        public int cantidad {get; set;}
        public EstadoRequerimiento? estado {get; set;}
        public Guid cotizaciones {get; set;} 
        public Parte parte {get; set;}
        
    }
    public enum TipoRequerimiento
    {
        Reparacion,
        ComprarPieza
    }

    public enum EstadoRequerimiento
    {
        PorEntregar,
        Entregado
    }
}