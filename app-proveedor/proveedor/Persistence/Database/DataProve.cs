using proveedor.Persistence.Entities;
namespace proveedor.Persistence.Database
{
    public class DataProve
    {
        
        public List<SolicitudReparacion> solicitudRepInit = new List<SolicitudReparacion>();
        public List<Requerimiento> requerimientoInit = new List<Requerimiento>();
        public List<Parte> parteInit = new List<Parte>();
       

        public DataProve()
        {
            //getTallerData();
            //getVehiculoData();
            getSolicitudRepData();
            getPartesData();
            getRequerimientosData();
            //getMarcaTallerData();
        }
        /*public void getTallerData()
        {
            this.tallerInit.Add( new Taller{
                    tallerId = Guid.Parse("10003262-d5ef-46c7-bc0e-97530823c05b"),
                    nombreLocal = "Taller de Pepito"
                }
            );
            this.tallerInit.Add( new Taller{
                    tallerId = Guid.Parse("20003262-d5ef-46c7-bc0e-97530823c05b"),
                    nombreLocal = "MadMonkey"
                }
            );
            this.tallerInit.Add( new Taller{
                    tallerId = Guid.Parse("30003262-d5ef-46c7-bc0e-97530823c05b"),
                    nombreLocal = "Locos Por los Autos"
                }
            );
            this.tallerInit.Add( new Taller{
                    tallerId = Guid.Parse("40003262-d5ef-46c7-bc0e-97530823c05b"),
                    nombreLocal = "Roller Customs"
                }
            );
        }
        public void getVehiculoData()
        {
            this.vehiculoInit.Add( new Vehiculo
            {
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c03b"),
                anioModelo = 2004,
                fechaCompra = DateTime.ParseExact("20-06-2018", "dd-MM-yyyy",null),
                color = Color.Verde,
                placa = "AB320AM",
                marca = Marca.Toyota,
            });
            vehiculoInit.Add(new Vehiculo(){
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c05b"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c03f"),
                anioModelo = 2006,
                fechaCompra = DateTime.ParseExact("15-06-2010", "dd-MM-yyyy",null),
                color = Color.Naranja,
                placa = "AB322AM",
                marca = Marca.Hyundai

            });
            
        }
        public void getMarcaTallerData()
        {
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-15ef-46c7-bc0e-97530821c04b"),
                tallerId = Guid.Parse("10003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Toyota,
            });
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-25ef-46c7-bc0e-97530821c04b"),
                tallerId=Guid.Parse("10003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Honda,
            });
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-35ef-46c7-bc0e-97530821c04b"),
                tallerId=Guid.Parse("20003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Hyundai,
            });
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-45ef-46c7-bc0e-97530821c04b"),
                tallerId=Guid.Parse("20003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Volkswagen,
            });
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-55ef-46c7-bc0e-97530821c04b"),
                tallerId=Guid.Parse("30003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Ford,
            });
            this.marcaInit.Add(new MarcaTaller(){
                marcaId = Guid.Parse("0c5c3262-65ef-46c7-bc0e-97530821c04b"),
                tallerId=Guid.Parse("30003262-d5ef-46c7-bc0e-97530823c05b"),
                marca = Marca.Ferrari,
            });
        }*/
        public void getSolicitudRepData()
        {
            this.solicitudRepInit.Add(new SolicitudReparacion()
            {
                solicitudRepId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                incidenteId = Guid.Parse("10000000-d5ef-46c7-bc0e-97530823c05b"),
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                tallerId = Guid.Parse("10003262-d5ef-46c7-bc0e-97530823c05b"),
                fechaSolicitud = new DateTime(2022,06,28),
            });
        }
        public void getPartesData()
        {
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-1000-97530821c04b"),
                nombre = "puerta izquierda delantera", 
            });
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-2000-97530821c04b"),
                nombre = "puerta derecha delantera", 
            });
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-3000-97530821c04b"),
                nombre = "rueda", 
            });
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-4000-97530821c04b"),
                nombre = "capó de motor", 
            });
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-5000-97530821c04b"),
                nombre = "capó de maleta", 
            });
            parteInit.Add( new Parte{
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-7000-97530821c04b"),
                nombre = "rin", 
            });
        }
        public void getRequerimientosData()
        {
            requerimientoInit.Add( new Requerimiento{
                requerimientoId = Guid.Parse("0c5c3262-d5ef-1000-bc0e-97530821c04b"),
                solicitudRepId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-2000-97530821c04b"),
                descripcion = "puerta detrozada parcialmente",
                tipoRequerimiento = TipoRequerimiento.Reparacion,
                cantidad = 1,
                estado = EstadoRequerimiento.PorEntregar, 
            });
            requerimientoInit.Add( new Requerimiento{
                requerimientoId = Guid.Parse("0c5c3262-d5ef-2000-bc0e-97530821c04b"),
                solicitudRepId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-5000-97530821c04b"),
                descripcion = "cambio capó de la maleta",
                tipoRequerimiento = TipoRequerimiento.ComprarPieza,
                cantidad = 1,
                estado = EstadoRequerimiento.PorEntregar, 
            });
        }
    }
}
