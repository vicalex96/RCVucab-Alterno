using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;

namespace taller.Conections.APIs
{
    public interface IServicioAPI
    {
        Task<TallerDTO> GetTaller(Guid tallerId); 
        /*        
        Task<MarcaDTO> GetMarcaTaller(Guid marcaTallerId);
        Task<CotizacionDTO> GetFactura(Guid cotizacionId);
        Task<SolicitudDTO> GetSolicitudData(Guid solicitudId);
        Task<VehiculoDTO> GetVehiculo(Guid solicitudId);
        */
    }
}