using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using administracion.Persistence.Entities;
using System.Text.Json.Serialization;

namespace administracion.BussinesLogic.DTOs
{
    /// <summary>
    ///DTO para mostra una marca
    /// </summary>
    public class MarcaDTO
    {
        public string nombreMarca {get; set;} = "";
    }
}