using proveedor.Persistence.Entities;
using proveedor.BussinesLogic.DTOs;
using System.Collections.Generic;

namespace proveedor.Persistence.DAOs.Interfaces
{
    public interface ICotizacionParteDAO
    {
    
        public string createCotizacionParte(CotizacionParteDTO cotPt);
        public List<CotizacionParteDTO> GetCotizacionPartes();
        public List<CotizacionParteDTO> GetCotizacionPartesByestado(EstadoCotPt estado);
        public string actualizarCotizacionParte(Guid CotizacionParteID, EstadoCotPt estado);
       
    }
}