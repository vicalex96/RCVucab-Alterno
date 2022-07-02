using Microsoft.EntityFrameworkCore;
using proveedor.Persistence.Entities;

namespace proveedor.Persistence.Database
{
    public class ProveedorDbContext : DbContext, IProveedorDbContext
    {


        public ProveedorDbContext(DbContextOptions<ProveedorDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }


        public virtual DbSet<CotizacionParteEntity> CotizacionPartes    {
            get;
            set;
        }

        public virtual DbSet<Parte> partes {get; set;}

        public virtual DbSet<Requerimiento> Requerimientos {get; set;}

        public virtual DbSet<SolicitudReparacion> SolicitudReparacions  {get; set;} 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            DataProve data = new DataProve();
            

            
            modelBuilder.Entity<SolicitudReparacion>(solicitud => {
                solicitud.HasKey(p => p.solicitudRepId);
                solicitud.HasData(data.solicitudRepInit);
            });
            modelBuilder.Entity<Requerimiento>(requer => {
                requer.HasKey(p => p.requerimientoId);
                requer.HasData(data.requerimientoInit);
            });

            
            
            modelBuilder.Entity<Parte>(parte =>{
                parte.HasKey(p => p.parteId);
                parte.HasData(data.parteInit);
            });
        }
    }

}