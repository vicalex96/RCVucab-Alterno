namespace administracion.BussinesLogic.LogicClasses
{
    public interface IVehiculoLogic
    {
        public bool AddAseguradoToVehiculo(Guid vehiculoId, Guid aseguradoId);
    }
}