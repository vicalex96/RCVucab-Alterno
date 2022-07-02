using Microsoft.EntityFrameworkCore;
using levantamiento.Persistence.Entities;
namespace levantamiento.Persistence.Database
{
    public interface ILevantamientoDBContext
    {
        public DbContext DbContext { get; }

        public DbSet<SolicitudReparacion> SolicitudesReparacion { get; set; }
        public DbSet<Requerimiento> Requerimientos { get; set; }
        public DbSet<Parte> Partes { get; set; }
        public DbSet<Incidente> Incidentes { get; set; }
    }
}