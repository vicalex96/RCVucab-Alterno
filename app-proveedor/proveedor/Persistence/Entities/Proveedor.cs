namespace proveedor.Persistence.Entities
{
    public class Proveedor
    {
        public Guid ProveedorId { get; set; }
        public string proveedorNombreLocal {get; set;}
        public ICollection<MarcaProveedor> marcas {get; set;}
        //public ICollection<SolicitudReparacion> solicitudes {get; set;}
    }
}