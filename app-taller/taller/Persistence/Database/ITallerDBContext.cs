using Microsoft.EntityFrameworkCore;
using taller.Persistence.Entities;
public interface ITallerDBContext
{
    DbContext DbContext { get;}
    DbSet<Taller> Talleres{get; set;}
    DbSet<MarcaTaller> Marcas {get; set;}
    DbSet<SolicitudReparacion> SolicitudReparacions  {get; set;}
    DbSet<Requerimiento> Requerimientos {get; set;}
    DbSet<Parte> partes {get; set;}
    DbSet<Vehiculo> Vehiculos {get; set;}
    DbSet<CotizacionReparacion> CotizacionReparaciones {get; set;}

}