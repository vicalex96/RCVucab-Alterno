using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using levantamiento.Persistence.Entities;


namespace levantamiento.BussinesLogic.DTOs
{
    public static class IncidenteDTOToEntity
    {
        public static Incidente ConvertDTOToEntity(IncidenteDTO Incidente)
        {
            return new Incidente
            {
                incidenteId = Incidente.Id,
                polizaId = Incidente.polizaId,
            };
        }

    }

}