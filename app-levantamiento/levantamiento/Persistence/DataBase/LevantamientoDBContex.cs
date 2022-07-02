using Microsoft.EntityFrameworkCore;
using levantamiento.Persistence.Entities;

namespace levantamiento.Persistence.Database
{
    public class LevantamientoDBContext: DbContext, ILevantamientoDBContext
    {
        public LevantamientoDBContext(){}

        public LevantamientoDBContext(DbContextOptions<LevantamientoDBContext> options) : base(options)
        {
        }
        
        public DbContext DbContext
        {
            get { return this; }
        }

        public DbSet<Incidente> Incidentes { get; set; }
        public DbSet<SolicitudReparacion> SolicitudesReparacion { get; set; }
        public DbSet<Requerimiento> Requerimientos { get; set; }
        public DbSet<Parte> Partes { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataProve data = new DataProve();

            modelBuilder.Entity<Incidente>(i =>
            {
                i.HasData(data.incidenteInit);
            });

            modelBuilder.Entity<SolicitudReparacion>(s =>
            {
                s.HasData(data.solicitudReparacionInit);
            });
            modelBuilder.Entity<Requerimiento>(r =>
            {
                r.HasData(data.requerimientoInit);
            });
            
            modelBuilder.Entity<Parte>(p =>
            {
                p.HasData(data.parteInit);
            });
            

            
        }

    }
}