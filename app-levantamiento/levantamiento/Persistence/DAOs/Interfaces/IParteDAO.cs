
using levantamiento.Persistence.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.Persistence.DAOs
{
    public interface IParteDAO
    {
        public List<ParteDTO> GetAll();
        public bool RegisterParte(ParteDTO Parte);
    }
}