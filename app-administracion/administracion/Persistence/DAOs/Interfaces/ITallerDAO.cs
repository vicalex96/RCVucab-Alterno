
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace administracion.Persistence.DAOs
{
    public interface ITallerDAO
    {
        
        public TallerDTO GetTallerByGuid (Guid tallerId);
        public List<TallerDTO> GetTalleres();

        public bool RegisterTaller (TallerSimpleDTO taller);
        public bool AddMarca(Guid tallerId, string marcaStr, bool todasLasMarcas = false);
        
    }
}