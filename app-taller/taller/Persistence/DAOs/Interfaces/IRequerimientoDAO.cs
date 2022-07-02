using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace taller.Persistence.DAOs
{
    public interface IRequerimientoDAO
    {
        
        public List<RequerimientoDTO> GetRequerimientos(Guid solicitudId);
        public bool UpdateTipoRequerimiento(Guid requerimientoId,TipoRequerimiento tipo);
        public bool UpdateQuantityPiecesRequerimiento(Guid requerimientoId,int quantity);
        public bool RegisterRequerimiento(RequerimientoDTO requerimiento);
    }


}