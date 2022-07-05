
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Taller
    /// </summary>
    public interface ITallerDAO
    {
        
        public TallerDTO GetTallerByGuid (Guid tallerId);
        public List<TallerDTO> GetTalleres();

        public Guid RegisterTaller (Taller taller);
        public bool AddMarca(MarcaTaller Marca);
        public bool DeleteMarcasFromTaller(Guid tallerId);
        
        public bool IsMarcaExistsOnTaller(Guid tallerId, Marca marca);
    }
}