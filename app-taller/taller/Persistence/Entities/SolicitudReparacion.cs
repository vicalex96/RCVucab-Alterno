namespace taller.Persistence.Entities
{
    public class SolicitudReparacion
    {
        public Guid solicitudRepId {get; set;}
        public Guid incidenteId {get; set;}
        public Guid vehiculoId {get; set;}
        public Guid tallerId {get; set;}
        public DateTime fechaSolicitud {get; set;}

        public Taller? taller {get; set;}
        public virtual List<Requerimiento>? requerimientos {get; set;}
        public virtual CotizacionReparacion? cotizacion {get; set;}
    }
}