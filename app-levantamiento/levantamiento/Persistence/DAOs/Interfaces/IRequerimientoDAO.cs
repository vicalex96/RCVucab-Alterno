
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IRequerimientoDAO
    {
        public bool RegisterRequerimiento(RequerimientoSimpleDTO requerimiento);

        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId);
    }
}