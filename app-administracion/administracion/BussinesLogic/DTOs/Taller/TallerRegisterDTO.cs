using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO crar un nuevo registro de taller en el sistema
    /// </summary>
    public class TallerRegisterDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;} ="";
    }
}
