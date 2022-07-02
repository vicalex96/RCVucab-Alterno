using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Entities;
namespace administracion.Persistence.Database
{
    public interface IAdminDBContext
    {
        DbContext DbContext { get; }

        DbSet<Asegurado> Asegurados { get; set; }
        DbSet<Vehiculo> Vehiculos { get; set; }
        DbSet<Poliza> Polizas { get; set; }
        DbSet<Incidente> Incidentes { get; set; }

        DbSet<Proveedor> Proveedores { get; set; }
        DbSet<Taller> Talleres { get; set; }
        DbSet<MarcaTaller> MarcasTaller { get; set; }
        DbSet<MarcaProveedor> MarcasProveedor { get; set; }
    }
}