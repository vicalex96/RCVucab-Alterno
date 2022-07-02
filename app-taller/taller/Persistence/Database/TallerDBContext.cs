using Microsoft.EntityFrameworkCore;
using taller.Persistence.Entities;

namespace taller.Persistence.Database
{
    public class TallerDBContext: DbContext, ITallerDBContext
    {
        public TallerDBContext(){}

        public TallerDBContext(DbContextOptions<TallerDBContext> options) : base(options)
        {
        }
        
        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }
        public virtual DbSet<Taller> Talleres{get; set;}
        public virtual DbSet<MarcaTaller> Marcas {get; set;}
        public virtual DbSet<SolicitudReparacion> SolicitudReparacions  {get; set;}
        public virtual DbSet<Vehiculo> Vehiculos {get; set;}
        public virtual DbSet<Requerimiento> Requerimientos {get; set;}
        public virtual DbSet<Parte> partes {get; set;}
        public virtual DbSet<CotizacionReparacion> CotizacionReparaciones {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            DataProve data = new DataProve();
            
            modelBuilder.Entity<Taller>( taller => {
                taller.HasKey(p => p.tallerId);
                taller.HasData(data.tallerInit);
            });
            modelBuilder.Entity<MarcaTaller>( marca => {
                marca.HasKey(p => p.marcaId);
                marca.HasData(data.marcaInit);
            });
            modelBuilder.Entity<SolicitudReparacion>(solicitud => {
                solicitud.HasKey(p => p.solicitudRepId);
                solicitud.HasData(data.solicitudRepInit);
            });
            modelBuilder.Entity<Requerimiento>(requer => {
                requer.HasKey(p => p.requerimientoId);
                requer.HasData(data.requerimientoInit);
            });
            modelBuilder.Entity<Vehiculo>(cot => {
                cot.HasKey(p => p.vehiculoId);
                cot.HasData(data.vehiculoInit);
            });
            modelBuilder.Entity<CotizacionReparacion>(cot => {
                cot.HasKey(p => p.cotizacionRepId);
                
                cot.HasOne(p => p.solicitud).WithOne(p => p.cotizacion).HasForeignKey<CotizacionReparacion>(p => p.solicitudRepId);
            });
            modelBuilder.Entity<Parte>(parte =>{
                parte.HasKey(p => p.parteId);
                parte.HasData(data.parteInit);
            });
        }

    }
}