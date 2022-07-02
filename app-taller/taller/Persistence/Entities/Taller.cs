namespace taller.Persistence.Entities
{
    public class Taller
    {
        public Guid tallerId { get; set; }
        public string nombreLocal {get; set;}
        public ICollection<MarcaTaller> marcas {get; set;}
        public ICollection<SolicitudReparacion> solicitudes {get; set;}
    }
}