using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Entities;

namespace administracion.Persistence.Database
{
    public class AdminDBContext: DbContext, IAdminDBContext
    {
        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }
        public virtual DbSet<Asegurado> Asegurados {get; set;}
        public virtual DbSet<Vehiculo> Vehiculos {get; set;}
        public virtual DbSet<Poliza> Polizas {get; set;}
        public virtual DbSet<Incidente> Incidentes {get; set;}

        public virtual DbSet<Proveedor> Proveedores {get; set;}
        public virtual DbSet<Taller> Talleres {get; set;}
        public virtual DbSet<MarcaTaller> MarcasTaller {get; set;}
        public virtual DbSet<MarcaProveedor> MarcasProveedor {get; set;}

        public AdminDBContext(){}

        public AdminDBContext(DbContextOptions<AdminDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            DataProve dataProve = new DataProve();
            
            modelBuilder.Entity<Incidente>(incidente =>
            {
                incidente.HasData(dataProve.incidenteInit);
            });

            modelBuilder.Entity<Poliza>(poliza =>
            {
                poliza.HasData(dataProve.polizaInit);
            });
            
            modelBuilder.Entity<Vehiculo>(vehiculo => 
            {
                vehiculo.HasData(dataProve.vehiculoInit);
            });

            modelBuilder.Entity<Asegurado>(asegurado => 
            {
                asegurado.HasData(dataProve.aseguradoInit);
            });

        }
    }
}