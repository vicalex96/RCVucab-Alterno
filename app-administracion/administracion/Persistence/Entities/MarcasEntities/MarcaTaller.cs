using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  administracion.Persistence.Entities
{
    public class MarcaTaller: MarcaEntity
    {
        [Required]
        public Guid tallerId { get; set; }
        [ForeignKey("tallerId")]
        public Taller? taller {get; set;}
    }
}
