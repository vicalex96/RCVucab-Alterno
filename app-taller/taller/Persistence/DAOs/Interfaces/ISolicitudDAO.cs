
using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace taller.Persistence.DAOs
{
    public interface ISolicitudDAO
    {
        public List<SolicitudDTO> GetSolicitudes();
        public bool UpdateList(ICollection<SolicitudQueueDTO> lista);
    }
}