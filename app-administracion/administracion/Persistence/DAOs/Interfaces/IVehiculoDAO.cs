
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Poliza
    /// </summary>
    public interface IVehiculoDAO
    {
        public List<VehiculoDTO> GetAllVehiculos();
        public VehiculoDTO GetVehiculoByGuid(Guid Id);
        public List<VehiculoDTO> GetVehiculosByAsegurado(Guid aseguradoId);
        public bool RegisterVehiculo(Vehiculo auto);
        public bool AddAsegurado(Guid vehiculoId, Guid aseguradoId);
        
    }
}