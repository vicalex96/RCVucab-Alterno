using levantamiento.BussinesLogic.DTOs;
using levantamiento.Conections.rabbit;
using levantamiento.Persistence.DAOs;
using levantamiento.Exceptions;

namespace levantamiento.BussinesLogic.Logic
{
    public class RequerimientoLogic: IRequerimientoLogic
    {
        private readonly IRequerimientoDAO _requerimientoDAO;
        private readonly ISolicitudReparacionDAO _solicitudDAO;
        private readonly IParteDAO _parteDAO;
        

        public RequerimientoLogic( IRequerimientoDAO requerimientoDAO, ISolicitudReparacionDAO solicitudDAO, IParteDAO parteDAO)
        {
            _requerimientoDAO = requerimientoDAO;
            _solicitudDAO = solicitudDAO;
            _parteDAO = parteDAO;
        }

        public bool RegisterRequerimiento(RequerimientoRegisterDTO requerimiento)
        {
            try
            {
                //Evitar continuar si la solicitud no existe
                if(_solicitudDAO.GetSolicitudById(requerimiento.solicitudId) == null)
                    throw new RCVNullException("Error: no se puede registra el requerimiento, la solicitud no existe");

                //Evita seguir con el proceso si no existe la parte indicada
                if(_parteDAO.GetParteById(requerimiento.parteId) == null)
                    throw new RCVNullException("Error: no se puede registrar el requerimiento, el pisea indicada no existe");
                
                //Si no se indico la cantidad de piesas requeridas o afectas tampoco prosigue
                if(requerimiento.cantidad <= 0)
                    throw new RCVInvalidFieldException("debe de indicar la cantidad de piesas requeridas");

                _requerimientoDAO.RegisterRequerimiento(
                    RequerimientoDTOToEntity.ConvertDTOToEntity(requerimiento)
                );
                return true;
            }
            catch(RCVException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error: No se logro registrar el requerimiento por algun error desconocido", ex);
            }
        }
        
    }
}