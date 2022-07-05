using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Conections.rabbit;
using administracion.Exceptions;

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
        public bool RegisterProveedor (ProveedorRegisterDTO proveedor)
        {
            try
            {
                //El nombre del local del proveedor no puede estar vacio
                if(proveedor.nombreLocal.ToLower() == "string" || 
                    proveedor.nombreLocal.Count() == 0)
                {
                    throw new Exception("Error");
                }
                Proveedor proveedorEntity = new Proveedor
                {
                    proveedorId = proveedor.Id,
                    nombreLocal = proveedor.nombreLocal,
                };

                //registra el proveedor en el sistema
                Guid Id = _proveedorDAO.RegisterProveedor(proveedorEntity);

                //envia la informacion a la cola de mensajes
                _productorRabbit.SendMessage(
                    Routings.proveedor,
                    "registrar_proveedor",
                    Id.ToString()
                );

                return true;
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
        public bool AddMarca(Guid proveedorId, string marcaStr)
        {
            try
            {
                Marca marca = new Marca();

                //Revisa que la marca introducida exita y la convierte de string a tipo Marca
                if(MarcaProveedor.IsMarca(marcaStr))
                    marca = MarcaProveedor.ConvertToMarca(marcaStr);
                else
                    throw new RCVException("La marca introducida no es valida");

                //Revisa que la no este registrada en el proveedor
                bool exists = _proveedorDAO.IsMarcaExistsOnProveedor(proveedorId, marca);

                if (exists)
                    throw new RCVException("El proveedor ya se especializa en dicha marca");

                MarcaProveedor marcaEntity = new MarcaProveedor
                {
                    marcaId = Guid.NewGuid(),
                    proveedorId = proveedorId,
                    manejaTodas = false,
                    marca = marca
                };
                return _proveedorDAO.AddMarca(marcaEntity);
            }
            catch (RCVException ex)
            {
                throw new RCVException(ex.Mensaje, ex);
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
        public bool AddAllMarcas(Guid proveedorId)
        {
            try
            {
                MarcaProveedor marcaEntity;

                //Borra los posible registros de marcas del proveedor
                bool success = _proveedorDAO.DeleteMarcasFromProveedor(proveedorId);

                if(!success)
                    throw new RCVException("Ocurrio un problema al intentar Limpriar la lista de especificaciones");
            
                //Genera una entidad marca que indique todas las marcas
                marcaEntity = new MarcaProveedor
                {
                    marcaId = Guid.NewGuid(),
                    proveedorId = proveedorId,
                    manejaTodas = true,
                };
                
                return _proveedorDAO.AddMarca(marcaEntity);
            }
            catch (RCVException ex)
            {
                throw new RCVException(ex.Mensaje, ex);
            }
            catch (Exception ex)
            {
                throw new RCVException("Error al registrar el incidente", ex);
            }
        }
    
    }
}