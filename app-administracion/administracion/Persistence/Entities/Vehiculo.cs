using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using administracion.Exceptions;

namespace administracion.Persistence.Entities
{
    public class Vehiculo
    {
        public Guid vehiculoId { get; set; }
        public int anioModelo { get; set; }
        public DateTime fechaCompra { get; set; }
        public Color color { get; set; }
        [MaxLength(7)]
        public string placa { get; set; }
        [Required]
        public Marca marca {get; set;}


        public Guid? aseguradoId {get; set;}
        public Asegurado? asegurado { get; set; } = null;
        public ICollection<Poliza>? polizas {get; set;} = null;

        public Vehiculo()
        {
        }
        public Vehiculo (Guid _Id, int _anioModelo, DateTime _fechaCompra, Color _color, string _placa, Marca _marca)
        {
            vehiculoId = _Id;
            anioModelo = _anioModelo;
            fechaCompra = _fechaCompra;
            color = _color;
            placa = _placa;
            marca = _marca;
        }

        public string validarPlaca(string _placa)
        {
            if(_placa.Length ==  7)
            {
                return _placa;
            }
            else 
            {
                throw new RCVException("La placa no cuenta con las especificaciones indicadas");
            }
            
        }

    }

    public enum Color
    {
        Rojo,
        Verde,
        Azul_oscuro,
        Azul_claro,
        Amarillo,
        Morado,
        Naranja,
        Marron,
        Violeta,
        Plateado,
        Dorado,
        Cobre,
        Blanco,
        Azul,
        Negro,

    }
}

    

