using Microsoft.EntityFrameworkCore;
using  administracion.DataAccess.Entities;

namespace  administracion.DataAccess.Database
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
        public virtual DbSet<Asegurado> Asegurados {get; set;}= null!;
        public virtual DbSet<Vehiculo> Vehiculos {get; set;}= null!;
        public virtual DbSet<Poliza> Polizas {get; set;}= null!;
        public virtual DbSet<Incidente> Incidentes {get; set;}= null!;

        public virtual DbSet<Proveedor> Proveedores {get; set;}= null!;
        public virtual DbSet<Taller> Talleres {get; set;}= null!;
        public virtual DbSet<MarcaTaller> MarcasTaller {get; set;}= null!;
        public virtual DbSet<MarcaProveedor> MarcasProveedor {get; set;} = null!;

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

            modelBuilder.Entity<Taller>(taller => 
            {
                taller.HasData(dataProve.tallerInit);
            });
            
            modelBuilder.Entity<MarcaTaller>(marca => 
            {
                marca.HasData(dataProve.marcasTallerInit);
            }); 

            modelBuilder.Entity<Proveedor>(proveedor => 
            {
                proveedor.HasData(dataProve.proveedorInit);
            });
            
            modelBuilder.Entity<MarcaProveedor>(marca => 
            {
                marca.HasData(dataProve.marcasProveedorInit);
            });           

        }
    }
}