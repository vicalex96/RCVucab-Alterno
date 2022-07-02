using levantamiento.Persistence.Entities;
namespace levantamiento.Persistence.Database
{
    public class DataProve
    {
        public List<Incidente> incidenteInit = new List<Incidente>();
        public List<Parte> parteInit = new List<Parte>();
        public List<Requerimiento> requerimientoInit = new List<Requerimiento>();
        public List<SolicitudReparacion> solicitudReparacionInit = new List<SolicitudReparacion>();
        
        public DataProve()
        {
            getIncidenteData();
            getPartesData();
            getRequerimientosData();
            GetSolicitudReparacionData();
        }
        public void getIncidenteData()
        {
            this.incidenteInit.Add(new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-bc01-97530821c04b"),
                polizaId=Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
            });
            this.incidenteInit.Add(new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-bc01-97530821c05b"),
                polizaId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04c"),
            });
            this.incidenteInit.Add(new Incidente(){
                incidenteId=Guid.Parse("0c5c3262-d5ef-46c7-bc01-97530821c06b"),
                polizaId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04d"),
            });
        }
    
        public void GetSolicitudReparacionData()
        {
            this.solicitudReparacionInit.Add(new SolicitudReparacion()
            {
                SolicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"),
                incidenteId = Guid.Parse("0c5c3262-d5ef-46c7-bc01-97530821c06b"),
                vehiculoId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c04b"),
                tallerId = Guid.Parse("10003262-d5ef-46c7-bc0e-97530823c05b"),
                fechaSolicitud = DateTime.ParseExact("15-06-2022", "dd-MM-yyyy",null),
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
                requerimientoId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c0c1"),
                solicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"),
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-2000-97530821c04b"),
                descripcion = "puerta detrozada parcialmente",
                tipoRequerimiento = TipoRequerimiento.Reparacion,
                cantidad = 1,
            });
            requerimientoInit.Add( new Requerimiento{
                requerimientoId = Guid.Parse("0c5c3262-d5ef-2000-bc0e-97530821c04b"),
                solicitudReparacionId = Guid.Parse("0c5c3262-d5ef-46c7-bc0e-97530821c0cc"),
                parteId = Guid.Parse( "0c5c3262-d5ef-46c7-5000-97530821c04b"),
                descripcion = "cambio capó de la maleta",
                tipoRequerimiento = TipoRequerimiento.ComprarPieza,
                cantidad = 1 
            });
        }
    }
}
