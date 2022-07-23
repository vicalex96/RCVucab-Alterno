using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using administracion.Persistence.Enums;

namespace  administracion.Persistence.Entities
{
    public class MarcaEntity
    {

        [Key]
        public Guid Id {get; set;}
        public bool manejaTodas {get; set;}= false;
        public MarcaName? marca {get; set;} = null;

        public MarcaEntity ()
        {
            Id = Guid.NewGuid();
        }

        public static bool IsMarca (string marca)
        {
            try{
                MarcaName result = (MarcaName)Enum.Parse(typeof(MarcaName), marca);
                return true;
            }catch(Exception)
            {
                return false;
            }
        }
        public static MarcaName ConvertToMarca (string marca)
        {
            return (MarcaName)Enum.Parse(typeof(MarcaName), marca);
        }
    }
}