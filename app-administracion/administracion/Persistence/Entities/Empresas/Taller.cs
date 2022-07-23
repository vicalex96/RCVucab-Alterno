using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class Taller:EmpresaBase
    {
        public ICollection<MarcaTaller>? marcas {get; set;}
    }
}