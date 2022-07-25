using administracion.BussinesLogic.DTOs;
using administracion.BussinesLogic.Mappers;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Commands
{
    public class RegisterIncidenteCommand: Command<int>
    {
        private int _result;
        private readonly IncidenteRegisterDTO _incidenteDTO;

        public RegisterIncidenteCommand( IncidenteRegisterDTO incidenteDTO)
        {
            _incidenteDTO = incidenteDTO;
        }
        
        public override void Execute()
        {
            IncidenteDAO dao = DAOFactory.createIncidenteDAO();
            
            _result = dao.RegisterIncidente(
                    IncidenteMapper.MapToEntity(_incidenteDTO)
                );
        }

        public override int GetResult()
        {
            return _result!;
        }
    }
}