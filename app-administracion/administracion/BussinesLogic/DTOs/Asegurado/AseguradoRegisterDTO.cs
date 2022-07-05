
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    /// DTO para el registro de asegurados
    /// </summary>
    public class AseguradoRegisterDTO
    {
        public Guid Id { get; set; }
        public string nombre { get; set; } = "";
        public string apellido {get; set;} = "";
    }
}