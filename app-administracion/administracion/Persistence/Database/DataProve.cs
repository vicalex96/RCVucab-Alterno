using administracion.Persistence.Entities;
namespace administracion.Persistence.Database
{
    public class DataProve
    {

        public List<Vehiculo> vehiculoInit = new List<Vehiculo>();
        public List<MarcaTaller> marcaInit = new List<MarcaTaller>();

        public List<Asegurado> aseguradoInit = new List<Asegurado>();

        public Incidente incidenteInit = new Incidente();
        public Poliza polizaInit = new Poliza();

        public DataProve()
        {
            getAseguradoData();
            getVehiculoData();
            GetPolizaData();
            GetIncidenteData();
        }
        public void getAseguradoData()
        {
            this.aseguradoInit.Add(new Asegurado(){
                aseguradoId=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000001"),
                nombre = "Luis Jose",
                apellido = "Ramirez Gimenez"
            }); 

            this.aseguradoInit.Add(new Asegurado(){
                aseguradoId=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000002"),
                nombre = "Manuel Diego",
                apellido = "Banderas Lopez"
            }); 
        }

        public void getVehiculoData()
        {
            this.vehiculoInit = new List<Vehiculo>();
            vehiculoInit.Add(new Vehiculo(){
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000001"),
                anioModelo = 2004,
                fechaCompra = DateTime.ParseExact("20-06-2018", "dd-MM-yyyy",null),
                color = Color.Verde,
                placa = "AB320AM",
                marca = Marca.Toyota,


            });
            vehiculoInit.Add(new Vehiculo(){
                vehiculoId=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000002"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000002"),
                anioModelo = 2006,
                fechaCompra = DateTime.ParseExact("15-06-2010", "dd-MM-yyyy",null),
                color = Color.Naranja,
                placa = "AB322AM",
                marca = Marca.Hyundai

            });
        }


        public void GetIncidenteData()
        {
            this.incidenteInit = new Incidente()
            {
                incidenteId=Guid.Parse("10000000-d5ef-46c7-0004-000000000001"),
                estadoIncidente = EstadoIncidente.Pendiente,
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000001"),
                fechaRegistrado = DateTime.ParseExact("15-06-2010", "dd-MM-yyyy",null),
            };
        }
        public void GetPolizaData()
        {
            
            this.polizaInit = new Poliza(){

                polizaId = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000001"),
                fechaRegistro =  DateTime.ParseExact("10-06-2020", "dd-MM-yyyy",null),
                fechaVencimiento = DateTime.ParseExact("10-06-2025", "dd-MM-yyyy",null),
                tipoPoliza = TipoPoliza.CoberturaCompleta,
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001")
            };
        }

        
    }
}
