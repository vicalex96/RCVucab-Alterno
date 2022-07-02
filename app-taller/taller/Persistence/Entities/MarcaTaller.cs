namespace  taller.Persistence.Entities
{
    public class MarcaTaller
    {
        public Guid marcaId { get; set; }
        public Guid tallerId { get; set; }
        public Taller? taller {get; set;}
        public bool manejaTodas {get; set;}= false;
        public Marca? marca {get; set;}
    }

    public enum Marca
    {
        Toyota,
        Honda,
        Volkswagen,
        Audi,
        BMW,
        Ford,
        Ferrari,
        Hyundai,
        General_Motors,
        Renault,
        Suzuki
        
    }
}
