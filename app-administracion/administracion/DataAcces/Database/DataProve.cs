using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

namespace  administracion.DataAccess.Database
{
    public class DataProve
    {

        public List<Vehiculo> vehiculoInit = new List<Vehiculo>();
        public List<MarcaTaller> marcaInit = new List<MarcaTaller>();

        public List<Asegurado> aseguradoInit = new List<Asegurado>();

        public List<Incidente> incidenteInit = new List<Incidente>();
        public List<Poliza> polizaInit = new List<Poliza>();

        public List<Taller> tallerInit = new List<Taller>();
        public List<MarcaTaller> marcasTallerInit = new List<MarcaTaller>();
        public List<Proveedor> proveedorInit = new List<Proveedor>();
        public List<MarcaProveedor> marcasProveedorInit = new List<MarcaProveedor>();

        public DataProve()
        {
            getAseguradoData();
            getVehiculoData();
            GetPolizaData();
            GetIncidenteData();
            GetTallerData();
            GetMarcasTallerData();
            GetProveedorData();
            GetMarcasProveedorData();
            
        }
        public void getAseguradoData()
        {
            this.aseguradoInit.Add(new Asegurado(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000001"),
                nombre = "Luis Jose",
                apellido = "Ramirez Gimenez"
            }); 

            this.aseguradoInit.Add(new Asegurado(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000002"),
                nombre = "Manuel Diego",
                apellido = "Banderas Lopez"
            }); 
            this.aseguradoInit.Add(new Asegurado(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000003"),

                nombre = "Daniel",
                apellido = "Gimenez"
            }); 
            this.aseguradoInit.Add(new Asegurado(){

                Id=Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000004"),

                nombre = "Maria Jose",
                apellido = "Salaguchi"
            }); 
        }
        public void getVehiculoData()
        {
            this.vehiculoInit = new List<Vehiculo>();
            vehiculoInit.Add(new Vehiculo(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000001"),
                anioModelo = 2007,
                fechaCompra = DateTime.ParseExact("20-06-2007", "dd-MM-yyyy",null),
                color = Color.Verde,
                placa = "AB320AM",

                marca = MarcaName.Toyota,


            });
            vehiculoInit.Add(new Vehiculo(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000002"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000002"),
                anioModelo = 2006,
                fechaCompra = DateTime.ParseExact("15-06-2014", "dd-MM-yyyy",null),
                color = Color.Naranja,
                placa = "AB322AM",

                marca = MarcaName.Hyundai
            });
            vehiculoInit.Add(new Vehiculo(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000003"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000003"),
                anioModelo = 2016,
                fechaCompra = DateTime.ParseExact("15-06-2017", "dd-MM-yyyy",null),
                color = Color.Morado,
                placa = "BB322AC",
                marca = MarcaName.General_Motors
            });
            vehiculoInit.Add(new Vehiculo(){
                Id=Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000004"),
                aseguradoId= Guid.Parse("0c5c3262-d5ef-46c7-0001-000000000003"),
                anioModelo = 2020,
                fechaCompra = DateTime.ParseExact("15-06-2020", "dd-MM-yyyy",null),
                color = Color.Plateado,
                placa = "BB329AC",
                marca = MarcaName.BMW
            });

        }
        public void GetPolizaData()
        {
            
            this.polizaInit = new List<Poliza>();
            
            polizaInit.Add(new Poliza(){


                Id = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000001"),

                fechaRegistro =  DateTime.ParseExact("10-06-2009", "dd-MM-yyyy",null),
                fechaVencimiento = DateTime.ParseExact("10-06-2014", "dd-MM-yyyy",null),
                tipoPoliza = TipoPoliza.CoberturaCompleta,
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000001")
            });
            polizaInit.Add(new Poliza(){


                Id = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000002"),

                fechaRegistro =  DateTime.ParseExact("10-06-2016", "dd-MM-yyyy",null),
                fechaVencimiento = DateTime.ParseExact("10-06-2020", "dd-MM-yyyy",null),
                tipoPoliza = TipoPoliza.DaniosATerceros,
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000002")
            });
            polizaInit.Add(new Poliza(){

                Id = Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000003"),

                fechaRegistro =  DateTime.ParseExact("10-06-2020", "dd-MM-yyyy",null),
                fechaVencimiento = DateTime.ParseExact("10-06-2025", "dd-MM-yyyy",null),
                tipoPoliza = TipoPoliza.CoberturaCompleta,
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-0002-000000000003")
            });
        }

        public void GetIncidenteData()
        {
            this.incidenteInit = new List<Incidente>();
            incidenteInit.Add(new Incidente()
            {

                Id=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000001"),

                estadoIncidente = EstadoIncidente.Pendiente,
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000001"),
                fechaRegistrado = DateTime.ParseExact("30-06-2010", "dd-MM-yyyy",null),
            });
            incidenteInit.Add(new Incidente()
            {

                Id=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000002"),

                estadoIncidente = EstadoIncidente.Pendiente,
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000002"),
                fechaRegistrado = DateTime.ParseExact("13-08-2018", "dd-MM-yyyy",null),
            });
            incidenteInit.Add(new Incidente()
            {

                Id=Guid.Parse("0c5c3262-d5ef-46c7-0004-000000000003"),

                estadoIncidente = EstadoIncidente.Pendiente,
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-0003-000000000003"),
                fechaRegistrado = DateTime.ParseExact("07-12-2021", "dd-MM-yyyy",null),
            });
        }

        public void GetTallerData()
        {
            this.tallerInit = new List<Taller>();
            tallerInit.Add(new Taller{

                Id = Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000001"),
                nombreLocal = "Gas Monkey",
            });
            tallerInit.Add(new Taller{
                Id = Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000002"),

                nombreLocal = "Taller de Luis",
            });
        }

        public void GetMarcasTallerData()
        {
            this.marcasTallerInit = new List<MarcaTaller>();
            marcasTallerInit.Add(new MarcaTaller{

                Id = Guid.Parse("00000001-d5ef-46c7-0005-000000000001"),
                tallerId =  Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.General_Motors,
            });
            marcasTallerInit.Add(new MarcaTaller{
                Id = Guid.Parse("00000002-d5ef-46c7-0005-000000000001"),
                tallerId =  Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.BMW,
            });
            marcasTallerInit.Add(new MarcaTaller{
                Id = Guid.Parse("00000003-d5ef-46c7-0005-000000000001"),
                tallerId =  Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.Hyundai,
            });
            marcasTallerInit.Add(new MarcaTaller{
                Id = Guid.Parse("00000001-d5ef-46c7-0005-000000000002"),

                tallerId =  Guid.Parse("0c5c3262-d5ef-46c7-0005-000000000002"),   
                manejaTodas = true,
                marcaName = null,
            });
            
        }

        public void GetProveedorData()
        {
            this.proveedorInit = new List<Proveedor>();
            proveedorInit.Add(new Proveedor{
                Id = Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000001"),
                nombreLocal = "Todo en partes 3000",
            });
            proveedorInit.Add(new Proveedor{
                Id = Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000002"),
                nombreLocal = "Tu Carro, tu repuesto",
            });
        }
        public void GetMarcasProveedorData()
        {
            this.marcasProveedorInit = new List<MarcaProveedor>();
            marcasProveedorInit.Add(new MarcaProveedor{
                Id = Guid.Parse("00000001-d5ef-46c7-0006-000000000001"),
                proveedorId =  Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.General_Motors,
            });
            marcasProveedorInit.Add(new MarcaProveedor{
                Id = Guid.Parse("00000002-d5ef-46c7-0006-000000000001"),
                proveedorId =  Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.Ford,
            });
            marcasProveedorInit.Add(new MarcaProveedor{
                Id = Guid.Parse("00000003-d5ef-46c7-0006-000000000001"),
                proveedorId =  Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.Hyundai,
            });
            marcasProveedorInit.Add(new MarcaProveedor{
                Id = Guid.Parse("00000004-d5ef-46c7-0006-000000000001"),
                proveedorId =  Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000001"),   
                manejaTodas = false,
                marcaName = MarcaName.Toyota,
            });
            marcasProveedorInit.Add(new MarcaProveedor{
                Id = Guid.Parse("00000001-d5ef-46c7-0006-000000000002"),
                proveedorId =  Guid.Parse("0c5c3262-d5ef-46c7-0006-000000000002"),   
                manejaTodas = true,
                marcaName = null,
            });
            
        }
        
    }
}
