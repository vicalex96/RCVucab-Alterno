
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;


namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Poliza
    /// </summary>
    public interface IPolizaDAO
    {
        public PolizaDTO GetPolizaByGuid(Guid polizaId);
        public PolizaDTO GetPolizaByVehiculoGuid(Guid vehiculoID);
        public bool RegisterPoliza (Poliza poliza);
    }
}