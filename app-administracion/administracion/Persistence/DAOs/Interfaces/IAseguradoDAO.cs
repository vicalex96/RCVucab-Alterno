
using administracion.Persistence.Entities;
using administracion.BussinesLogic.DTOs;

namespace administracion.Persistence.DAOs
{
    /// <summary>
    /// Interface para el listado de metodos de DAO de Asegurado
    /// </summary>
    public interface IAseguradoDAO
    {
        public AseguradoDTO GetAseguradoByGuid(Guid Id);
        public List<AseguradoDTO> GetAsegurados();
        public List<AseguradoDTO> GetAseguradosPorNombreCompleto(string nombre, string apellido);
        public bool RegisterAsegurado(Asegurado asegurado);
    }
}