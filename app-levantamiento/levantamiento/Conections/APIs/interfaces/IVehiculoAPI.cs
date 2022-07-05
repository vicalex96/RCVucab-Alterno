using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using levantamiento.Responses;

namespace levantamiento.Conections.APIs
{
    public interface IVehiculoAPI
    {
        public Task<bool> RegisterVehiculo(VehiculoRegisterDTO vehiculo);
        public Task<VehiculoDTO> GetVehiculoFromAdmin(Guid vehiculoId);
    }
}