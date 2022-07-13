
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IParteDAO
    {
        public List<ParteDTO> GetAll();
        public ParteDTO GetParteById(Guid parteId);
        public bool RegisterParte(Parte parte);
    }
}