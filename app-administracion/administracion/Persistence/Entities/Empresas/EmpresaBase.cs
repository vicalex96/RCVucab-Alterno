
using System.ComponentModel.DataAnnotations;

namespace administracion.Persistence.Entities
{
    public class EmpresaBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string nombreLocal {get; set;} ="";
        

        public EmpresaBase ()
        {
            Id = Guid.NewGuid();
        }
    }
}