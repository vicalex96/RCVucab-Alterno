using administracion.BussinesLogic.DTOs;

namespace administracion.BussinesLogic.LogicClasses
{
    public interface IVehiculoLogic
    {
        public int RegisterVehiculo(VehiculoRegisterDTO vehiculo);
        public int AddAseguradoToVehiculo(Guid vehiculoId, Guid aseguradoId);

        
    }
}