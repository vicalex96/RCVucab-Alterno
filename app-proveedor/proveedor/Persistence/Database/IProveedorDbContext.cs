using Microsoft.EntityFrameworkCore;
using proveedor.Persistence.Entities;

namespace proveedor.Persistence.Database
{
    public interface IProveedorDbContext
    {
        DbContext DbContext
        {
            get;
        }



        DbSet<CotizacionParteEntity> CotizacionPartes
        {
            get; set;
        }
        DbSet<Requerimiento> Requerimientos {get; set;}
        DbSet<Parte> partes {get; set;}
        DbSet<SolicitudReparacion> SolicitudReparacions  {get; set;} 
    }
}
