using taller.Persistence.Entities;
using taller.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace taller.Persistence.DAOs
{
    public interface IParteDAO
    {
        public List<ParteDTO> GetPartes();
        public bool RegisterParte(ParteDTO parte);
        
    }


}