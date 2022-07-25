
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.DataAccess.DAOs
{
    public interface IRequerimientoDAO
    {
        public Guid RegisterRequerimiento(Requerimiento requerimiento);

        public List<RequerimientoDTO> GetRequerimientosBySolicitudId(Guid solicitudId);
    }
}