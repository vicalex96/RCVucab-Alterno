
using  administracion.DataAccess.Entities;
using administracion.BussinesLogic.DTOs;


namespace  administracion.DataAccess.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Poliza
    /// </summary>
    public interface IPolizaDAO
    {
        public PolizaDTO GetPolizaById(Guid polizaId);
        public PolizaDTO GetPolizaByVehiculoId(Guid vehiculoID);
        public int RegisterPoliza (Poliza poliza);
    }
}