using Microsoft.EntityFrameworkCore;
using administracion.Persistence.Entities;

namespace administracion.Persistence.Database
{
    public class AdminDBContext: DbContext, IAdminDBContext
    {
        public AdminDBContext(){}

        public AdminDBContext(DbContextOptions<AdminDBContext> options) : base(options)
        {
        }
        
        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }
        public virtual  DbSet<Asegurado> Asegurados {get; set;}
        public virtual DbSet<Vehiculo> Vehiculos {get; set;}
        public virtual DbSet<Poliza> Polizas {get; set;}
        public virtual DbSet<Incidente> Incidentes {get; set;}

        public virtual DbSet<Proveedor> Proveedores {get; set;}
        public virtual DbSet<Taller> Talleres {get; set;}
        public virtual DbSet<MarcaTaller> MarcasTaller {get; set;}
        public virtual DbSet<MarcaProveedor> MarcasProveedor {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            List<Asegurado> aseguradoInit = new List<Asegurado>();
            aseguradoInit.Add(new Asegurado(){
                aseguradoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c03b"),
                nombre = "Luis Jose",
                apellido = "Ramirez Gimenez"
            }); 

            aseguradoInit.Add(new Asegurado(){
                aseguradoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c03f"),
                nombre = "Manuel Diego",
                apellido = "Banderas Lopez"
            }); 
            
            List<Vehiculo> vehiculoInit = new List<Vehiculo>();
            vehiculoInit.Add(new Vehiculo(){
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                aseguradoId= aseguradoInit[0].aseguradoId,
                anioModelo = 2004,
                fechaCompra = DateTime.ParseExact("20-06-2018", "dd-MM-yyyy",null),
                color = Color.Verde,
                placa = "AB320AM",
                marca = Marca.Toyota,


            });
            vehiculoInit.Add(new Vehiculo(){
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                aseguradoId= aseguradoInit[1].aseguradoId,
                anioModelo = 2006,
                fechaCompra = DateTime.ParseExact("15-06-2010", "dd-MM-yyyy",null),
                color = Color.Naranja,
                placa = "AB322AM",
                marca = Marca.Hyundai

            });
            Poliza polizaInit = new Poliza(){
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530823c05b"),
                fechaRegistro =  DateTime.ParseExact("10-06-2020", "dd-MM-yyyy",null),
                fechaVencimiento = DateTime.ParseExact("10-06-2025", "dd-MM-yyyy",null),
                tipoPoliza = TipoPoliza.CoberturaCompleta,
                vehiculoId = vehiculoInit[0].vehiculoId
            };

            Incidente incidenteInit = new Incidente()
            {
                incidenteId=Guid.Parse("10000000-d5ef-46c7-bc0e-97530823c05b"),
                polizaId=polizaInit.polizaId,
            };

            modelBuilder.Entity<Asegurado>(asegurado => 
            {
                asegurado.HasKey(p => p.aseguradoId);
                asegurado.Property(p => p.nombre)
                        .IsRequired()
                        .HasMaxLength(100);
                asegurado.Property(p => p.apellido)
                        .IsRequired()
                        .HasMaxLength(100);
                asegurado.HasData(aseguradoInit);
            });
            modelBuilder.Entity<Vehiculo>(vehiculo => 
            {
                vehiculo.HasKey(p => p.vehiculoId);
                vehiculo.HasData(vehiculoInit);

            });

            modelBuilder.Entity<Poliza>(poliza =>
            {
                poliza.HasKey(p => p.polizaId);
                poliza.HasData(polizaInit);
            });

            modelBuilder.Entity<Incidente>(incidente =>
            {
                incidente.HasKey(p => p.incidenteId);
                incidente.HasData(incidenteInit);
            });

            modelBuilder.Entity<Taller>(t =>
            {
                t.HasKey(p => p.tallerId);
            });

            modelBuilder.Entity<Proveedor>(t =>
            {
                t.HasKey(p => p.proveedorId);
            });
            
            modelBuilder.Entity<MarcaTaller>(marca =>
            {
                marca.HasKey(p => new {p.marcaId, p.tallerId});
            });

            modelBuilder.Entity<MarcaProveedor>(marca =>
            {
                marca.HasKey(p =>new {p.marcaId, p.proveedorId});
            });

        }


    }
}