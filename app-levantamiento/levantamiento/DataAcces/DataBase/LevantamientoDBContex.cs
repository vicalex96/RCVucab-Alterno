using Microsoft.EntityFrameworkCore;
using levantamiento.DataAccess.Entities;

namespace levantamiento.DataAccess.Database
{
    public class LevantamientoDBContext: DbContext, ILevantamientoDBContext
    {
        public DbSet<Incidente> Incidentes { get; set; }= null!;
        public DbSet<SolicitudReparacion> SolicitudesReparacion { get; set; }= null!;
        public DbSet<Requerimiento> Requerimientos { get; set; } = null!;
        public DbSet<Parte> Partes { get; set; } = null!;
        public LevantamientoDBContext(){}

        public LevantamientoDBContext(DbContextOptions<LevantamientoDBContext> options) : base(options)
        {
        }
        
        public DbContext DbContext
        {
            get { return this; }
        }


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