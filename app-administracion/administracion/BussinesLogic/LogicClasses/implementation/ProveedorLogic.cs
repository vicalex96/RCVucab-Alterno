using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Conections.rabbit;
using administracion.Exceptions;
using administracion.Persistence.Enums;
using administracion.BussinesLogic.Mappers;

namespace administracion.BussinesLogic.LogicClasses
{
    public class ProveedorLogic: IProveedorLogic
    {
        private readonly IProveedorDAO _proveedorDAO;
        private readonly IProductorRabbit _productorRabbit;

        public ProveedorLogic (IProveedorDAO proveedorDao, IProductorRabbit productorRabbit)
        {
            _proveedorDAO = proveedorDao;
            _productorRabbit = productorRabbit;
        }

        /// <summary>
        /// registra un proveedor en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="proveedor">DTO de registro con la data de proveedor</param>
        /// <returns>boleano true si todo salio bien</returns>
        public int  RegisterProveedor (ProveedorRegisterDTO proveedor)
        {
            try
            {
                //El nombre del local del proveedor no puede estar vacio
                if(proveedor.nombreLocal.ToLower() == "string" || 
                    proveedor.nombreLocal.Count() == 0)
                {
                    throw new RCVInvalidFieldException("Error: el nombre del local no puede estar vacio o por defecto");
                }

                //registra el proveedor en el sistema
                int result= _proveedorDAO.RegisterProveedor(
                                ProveedorMapper.MapToEntity(proveedor)
                            );

                /*//envia la informacion a la cola de mensajes
                _productorRabbit.SendMessage(
                    Routings.proveedor,
                    "registrar_proveedor",
                    proveedor.Id.ToString()
                );*/

                return result;
            }
            catch(RCVInvalidFieldException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al registrar el proveedor", ex);
            }
        }
        
        /// <summary>
        /// Agrega una o todas las marcas al proveedor
        /// </summary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <param name="marcaStr">nombre de la marca</param>
        /// <returns>boleano true si todo salio bien</returns>
        public int AddMarca(Guid proveedorId, string marcaStr)
        {
            try
            {
                MarcaName marca = new MarcaName();

                //Revisa que la marca introducida exita y la convierte de string a tipo Marca
                marca = (MarcaName) Enum.Parse(typeof(MarcaName), marcaStr);

                //Revisa que la no este registrada en el proveedor
                bool exists = _proveedorDAO.IsMarcaExistsOnProveedor(proveedorId, marca);

                if (exists)
                    throw new RCVAsociationException("El proveedor ya se especializa en dicha marca");

                MarcaProveedor marcaEntity = new MarcaProveedor
                {
                    Id = Guid.NewGuid(),
                    proveedorId = proveedorId,
                    manejaTodas = false,
                    marca = marca
                };
                return _proveedorDAO.AddMarca(marcaEntity);
            }
            catch(ArgumentException ex)
            {
                throw new RCVInvalidFieldException("La marca introducida no existe en el sistema o esta mal escrita", ex);
            }
            catch (RCVAsociationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el incidente", ex);
            }
        }

        /// <summary>
        /// indica que in proveedor se especializara en todas las marcas
        /// </sumary>
        /// <param name="proveedorId">Id del proveedor</param>
        /// <returns>boleano true si todo salio bien</returns>
        public int AddAllMarcas(Guid proveedorId)
        {
            try
            {
                MarcaProveedor marcaEntity;

                //Borra los posible registros de marcas del proveedor
                _proveedorDAO.DeleteMarcasFromProveedor(proveedorId);
            
                //Genera una entidad marca que indique todas las marcas
                marcaEntity = new MarcaProveedor
                {
                    Id = Guid.NewGuid(),
                    proveedorId = proveedorId,
                    manejaTodas = true,
                };
                
                return _proveedorDAO.AddMarca(marcaEntity);
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el incidente", ex);
            }
        }
    
    }
}