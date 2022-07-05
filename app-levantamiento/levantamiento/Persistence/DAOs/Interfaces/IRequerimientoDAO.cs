
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IRequerimientoDAO
    {
        public bool RegisterRequerimiento(RequerimientoRegisterDTO requerimiento);

        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId);
    }
}