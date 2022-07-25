using levantamiento.DataAccess.DAOs;

namespace levantamiento.DataAccess.DAOs
{
    public class DAOFactory
    {
        public static IncidenteDAO createIncidenteDAO()
        {
            return new IncidenteDAO();
        }

        public static ParteDAO createParteDAO()
        {
            return new ParteDAO();
        }

        public static RequerimientoDAO createRequerimientoDAO()
        {
            return new RequerimientoDAO();
        }

        public static SolcitudReparacionDAO createSolcitudReparacionDAO()
        {
            return new SolcitudReparacionDAO();
        }

    }
}