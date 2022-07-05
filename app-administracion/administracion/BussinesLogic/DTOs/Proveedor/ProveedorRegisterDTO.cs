using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO para registrar proveedor
    /// </summary>
    public class ProveedorRegisterDTO
    {
        public Guid Id { get; set; }
        public string nombreLocal {get; set;} ="";
    }
}
