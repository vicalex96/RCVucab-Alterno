using administracion.BussinesLogic.DTOs;
using administracion.Persistence.DAOs;
using administracion.Exceptions;
using administracion.Persistence.Entities;

namespace administracion.BussinesLogic.LogicClasses
{
    public class PolizaLogic: IPolizaLogic
    {
        private readonly IVehiculoDAO _vehiculoDao;
        private readonly IPolizaDAO _polizaDAO;

        public PolizaLogic (IVehiculoDAO vehiculoDao,IAseguradoDAO aseguradoDao, IPolizaDAO polizaDAO)
        {
            _vehiculoDao = vehiculoDao;
            _polizaDAO = polizaDAO;
        }
        
        /// <summary>
        /// registra una poliza en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="poliza">DTO de registro con la data de poliza</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool RegisterPoliza(PolizaRegisterDTO poliza)
        {
            try
            {
                // vehiculo tiene no puede tener polizas activas
                PolizaDTO polizaDTO = _polizaDAO.GetPolizaByVehiculoGuid(poliza.vehiculoId);
                if(polizaDTO != null)
                {
                    throw new RCVException("El vehiculo ya tiene una poliza registrada y activa");
                }

                // el vehiculo tiene que estar asignado a un asegurado
                VehiculoDTO vehiculoDTO = _vehiculoDao.GetVehiculoByGuid(poliza.vehiculoId);
                if(vehiculoDTO.asegurado == null)
                {
                    throw new RCVException("El vehiculo no tiene un asegurado asignado");
                }

                // El rango de fechas de la poliza debe ser correcto
                if(poliza.fechaVencimiento < poliza.fechaRegistro)
                {
                    throw new RCVException("La fecha de vencimiento debe ser mayor a la fecha de registro");
                }

                Poliza polizaEntity = new Poliza();

                polizaEntity.polizaId = poliza.Id;
                polizaEntity.fechaRegistro = poliza.fechaRegistro;
                polizaEntity.fechaVencimiento = poliza.fechaVencimiento;
                polizaEntity.tipoPoliza = (TipoPoliza)Enum.Parse(typeof(TipoPoliza), poliza.tipoPoliza);
                polizaEntity.vehiculoId = poliza.vehiculoId;

                return _polizaDAO.RegisterPoliza(polizaEntity);
            }   
            catch(RCVException ex)
            {
                throw new RCVException(ex.Mensaje, ex);
            }
            catch(Exception ex)
            {
                throw new RCVException("Ocurrio un problema al intentar registrar",ex);
            }
        }
    }
}