using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace taller.Persistence.DAOs
{
    public interface ITallerDAO
    {
        public TallerDTO GetTallerByGuid (Guid tallerId);
        public List<TallerDTO> GetTalleres(); 
        public string RegisterTallerPorAPI(TallerSimpleDTO taller);
        public string RegisterMarcasPorAPI(Guid tallerId, List<MarcaDTO> marcas);
    }
}