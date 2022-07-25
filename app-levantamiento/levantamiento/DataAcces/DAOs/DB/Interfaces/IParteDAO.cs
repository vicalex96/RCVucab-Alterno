
using levantamiento.DataAccess.Entities;
using levantamiento.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace levantamiento.DataAccess.DAOs
{
    public interface IParteDAO
    {
        public List<ParteDTO> GetAll();
        public ParteDTO GetParteById(Guid parteId);
        public Guid RegisterParte(Parte parte);
    }
}