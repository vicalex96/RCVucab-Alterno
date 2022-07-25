using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  administracion.DataAccess.Entities;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO crar un nuevo registro de taller en el sistema
    /// </summary>
    public class TallerRegisterDTO
    {
        public string nombreLocal {get; set;} ="";
    }
}
