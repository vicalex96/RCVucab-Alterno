namespace levantamiento.BussinesLogic.DTOs
{
    public class SolicitudesReparacionDTO
    {
        public Guid Id {get; set;}
        public Guid incidenteId {get; set;}
        public Guid vehiculoId {get; set;}
        public Guid tallerId {get; set;}

        public IncidenteDTO? incidente {get; set;}
        public VehiculoDTO? vehiculo {get; set;}
        public TallerDTO? taller {get; set;}
        public ICollection<RequerimientoDTO>? requerimientos {get; set;}
    }
}