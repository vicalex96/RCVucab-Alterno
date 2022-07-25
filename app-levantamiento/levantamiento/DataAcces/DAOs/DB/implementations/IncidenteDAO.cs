using Microsoft.EntityFrameworkCore;
using levantamiento.DataAccess.Database;
using levantamiento.DataAccess.Entities;
using levantamiento.Exceptions;
using levantamiento.BussinesLogic.DTOs;


namespace levantamiento.DataAccess.DAOs
{
    public class IncidenteDAO : IIncidenteDAO
    {
        private static DesignTimeDbContextFactory desing = new DesignTimeDbContextFactory();

        private ILevantamientoDBContext _context = desing.CreateDbContext(null);

        ///<summary>
        ///actualiza la lista de incidentes desde el sistema de administracion
        ///</summary>
        ///<param name="incidente">incidente a registrar</param>
        ///<returns>true si se registro correctamente, false si no</returns>
        public Guid RegisterIncidente(Incidente incidente)
        {
            try
            {
                _context.Incidentes.Add(incidente);
                _context.DbContext.SaveChanges();
                return incidente.Id;
            }
            catch (Exception)
            {
                throw new RCVException("Error al registrar el incidente");
            }
        }

        ///<summary>
        ///Muestra todos los incidentes
        ///</summary>
        ///<returns>lista de incidentes</returns>
        public ICollection<IncidenteToShowDTO> GetAll()
        {
            try
            {
                return _context.Incidentes
                .Include(s => s.solicitudes)
                .Select(i => new IncidenteToShowDTO
                {
                    Id = i.Id,
                    solicitudesRepacion = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Where(r => r.incidenteId == i.Id)
                    .Select(s => new SolicitudesReparacionDTO
                    {
                        Id = s.Id,
                        incidenteId = s.incidenteId,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///<summary>
        ///Muestra los incidentes que no tengan solicitudes asociadas
        ///</summary>
        ///<returns>lista de incidentes</returns>
        public ICollection<IncidenteToShowDTO> GetAllWithoutSolicitud()
        {
            try
            {
                return (from nc in _context.Incidentes
                    join s in _context.SolicitudesReparacion on nc.Id equals s.incidenteId into relacion
                    from s in relacion.DefaultIfEmpty()
                    where s == null
                    select new IncidenteToShowDTO
                    {
                        Id = nc.Id

                    }).ToList();
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }

        ///<summary>
        ///Realiza una busqueda por id del incidente y devuelve el incidente
        ///</summary>
        ///<param name="incidenteId">id del incidente</param>
        ///<returns>incidente</returns>
        public IncidenteDTO GetIncidenteById(Guid incidenteId)
        {
            try
            {
                return _context.Incidentes
                .Include(s => s.solicitudes)
                .Where(i => i.Id == incidenteId)
                .Select(i => new IncidenteDTO
                {
                    Id = i.Id,
                    solicitudes = _context.SolicitudesReparacion
                    .Include(r => r.requerimientos)
                    .Select(s => new SolicitudesReparacionDTO
                    {
                        Id = s.Id,
                        vehiculoId = s.vehiculoId,
                        tallerId = s.tallerId,
                    }).ToList()
                }).FirstOrDefault()!;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al obtener los incidentes", ex);
            }
        }
    }
}