using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IVehiculoLogic
    {
        public bool RegisterVehiculo(VehiculoRegisterDTO vehiculo);
        public bool AddAseguradoToVehiculo(Guid vehiculoId, Guid aseguradoId);

        
    }
}