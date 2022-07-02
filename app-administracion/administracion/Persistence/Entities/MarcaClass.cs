namespace  administracion.Persistence.Entities
{
    public class MarcaTaller
    {
        public Guid marcaId { get; set; }
        public Guid tallerId { get; set; }
        public Taller? taller {get; set;}
        public bool manejaTodas {get; set;}= false;
        public Marca? marca {get; set;}

        public static bool IsMarca (string marca)
        {
            try{
                Marca result = (Marca)Enum.Parse(typeof(Marca), marca);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public static Marca ConvertToMarca (string marca)
        {
            return (Marca)Enum.Parse(typeof(Marca), marca);
        }
    }
    public class MarcaProveedor
    {
        public Guid marcaId { get; set; }
        public Guid proveedorId { get; set; }
        public Proveedor? proveedor {get; set;}
        public bool manejaTodas {get; set;}= false;
        public Marca? marca {get; set;}

        public static bool IsMarca (string marca)
        {
            try{
                Marca result = (Marca)Enum.Parse(typeof(Marca), marca);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public static Marca ConvertToMarca (string marca)
        {
            return (Marca)Enum.Parse(typeof(Marca), marca);
        }
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
