
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IRequerimientoDAO
    {
        public bool RegisterRequerimiento(Requerimiento requerimiento);

        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId);
    }
}