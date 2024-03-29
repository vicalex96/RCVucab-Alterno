using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using levantamiento.DataAccess.Enums;

namespace levantamiento.DataAccess.Entities
{
    public class Requerimiento
    {
        [Key]
        public Guid Id {get; set;}
        
        [Required]
        public Guid solicitudReparacionId {get; set;}
        
        [Required]
        public Guid parteId {get; set;}

        public string descripcion {get; set;} = "";
        
        [Required] 
        public TipoRequerimiento tipoRequerimiento {get; set;}
        
        [Required]
        public int cantidad {get; set;}

        [ForeignKey("solicitudReparacionId")]
        public virtual SolicitudReparacion? solicitudReparacion {get; set;}
        
        [ForeignKey("parteId")]
        public Parte? parte {get; set;}

        public Requerimiento(){
            Id = Guid.NewGuid();
        }

        

        public static bool IsTipoRequerimiento (string tipo)
        {
            try{
                TipoRequerimiento result = (TipoRequerimiento)Enum.Parse(typeof(TipoRequerimiento), tipo);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        
        public static TipoRequerimiento ConvertToTipoRequerimiento (string tipo)
        {
            return ( TipoRequerimiento ) Enum
                .Parse( typeof(TipoRequerimiento), tipo );
        }
        
    }



}