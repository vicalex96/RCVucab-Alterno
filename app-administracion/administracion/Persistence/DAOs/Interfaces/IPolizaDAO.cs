
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace administracion.Persistence.DAOs
{
    public interface IPolizaDAO
    {
        public bool RegisterPoliza (PolizaSimpleDTO poliza);
        public PolizaDTO GetPolizaByGuid(Guid polizaId);
        public PolizaDTO GetPolizaByVehiculoGuid(Guid vehiculoID);
    }
}