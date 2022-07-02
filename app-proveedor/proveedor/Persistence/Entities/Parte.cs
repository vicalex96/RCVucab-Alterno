namespace proveedor.Persistence.Entities
{
    public class Parte
    {
        public Guid parteId {get; set;}
        public string nombre {get; set;}
        public ICollection<Requerimiento> requerimientos {get; set;} 
    }
}