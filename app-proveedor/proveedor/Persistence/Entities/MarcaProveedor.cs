namespace  proveedor.Persistence.Entities
{
    public class MarcaProveedor
    {
        public Guid marcaId { get; set; }
        public Guid tallerId { get; set; }
        public Proveedor? taller {get; set;}
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