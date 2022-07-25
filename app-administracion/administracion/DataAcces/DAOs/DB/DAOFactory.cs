using administracion.DataAccess.DAOs;

namespace administracion.DataAccess.DAOs
{
    public class DAOFactory
    {
        
        public static AseguradoDAO createAseguradoDAO()
        {
            return new AseguradoDAO();
        }
        
        public static IncidenteDAO createIncidenteDAO()
        {
            return new IncidenteDAO();
        }

        public static PolizaDAO createPolizaDAO()
        {
            return new PolizaDAO();
        }

        public static ProveedorDAO createProveedorDAO()
        {
            return new ProveedorDAO();
        }

        public static TallerDAO createTallerDAO()
        {
            return new TallerDAO();
        }

        public static VehiculoDAO createVehiculoDAO()
        {
            return new VehiculoDAO();
        }

    }
}