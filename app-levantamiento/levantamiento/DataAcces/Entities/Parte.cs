using System.ComponentModel.DataAnnotations;

namespace levantamiento.DataAccess.Entities
{
    public class Parte
    {
        [Key]
        public Guid Id {get; set;}
        [Required]
        public string nombre {get; set;} = "";

        public Parte(){
            Id = Guid.NewGuid();
        }
    }
}

