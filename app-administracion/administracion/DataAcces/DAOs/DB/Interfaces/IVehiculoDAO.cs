
using  administracion.DataAccess.Entities;
using administracion.BussinesLogic.DTOs;

namespace  administracion.DataAccess.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Poliza
    /// </summary>
    public interface IVehiculoDAO
    {
        public List<VehiculoDTO> GetAllVehiculos();
        public VehiculoDTO GetVehiculoByGuid(Guid Id);
        public List<VehiculoDTO> GetVehiculosByAsegurado(Guid aseguradoId);
        public int RegisterVehiculo(Vehiculo auto);
        public int AddAsegurado(Guid vehiculoId, Guid aseguradoId);
        
    }
}