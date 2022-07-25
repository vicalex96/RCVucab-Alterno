using administracion.BussinesLogic.DTOs;
using  administracion.DataAccess.Entities;
using  administracion.DataAccess.Enums;

namespace administracion.BussinesLogic.Mappers
{
    public class VehiculoMapper
    {
        public static Vehiculo MapToEntity(VehiculoDTO dto)
        {
            return new Vehiculo 
            {
                Id = dto.Id,
                anioModelo = dto.anioModelo,
                fechaCompra = dto.fechaCompra,
                color = (Color)Enum.Parse(typeof(Color), dto.color),
                placa= dto.placa,
                marca = (MarcaName)Enum.Parse(typeof(MarcaName), dto.marca),
            };
        }
        
        public static Vehiculo MapToEntity( VehiculoRegisterDTO dto)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.anioModelo = dto.anioModelo;
            vehiculo.fechaCompra = dto.fechaCompra;
            vehiculo.color = (Color)Enum.Parse(typeof(Color), dto.color);
            vehiculo.placa = dto.placa;
            vehiculo.marca = (MarcaName)Enum.Parse(typeof(MarcaName), dto.marca);
            return vehiculo;
            
        }

        public static VehiculoDTO MapToDTO (Vehiculo entity)
        {
            return new VehiculoDTO
            {
                Id = entity.Id,
                anioModelo = entity.anioModelo,
                fechaCompra = entity.fechaCompra,
                color = entity.color.ToString(),
                placa= entity.placa!,
                marca = entity.marca.ToString(),
            };
        }


    }
}