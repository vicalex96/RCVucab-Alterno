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
                    throw new RCVInvalidFieldException("Error: el nombre del local no puede estar vacio o por defecto");
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
        public bool AddMarca(Guid proveedorId, string marcaStr)
        {
            try
            {
                Marca marca = new Marca();

                //Revisa que la marca introducida exita y la convierte de string a tipo Marca
                marca = (Marca) Enum.Parse(typeof(Marca), marcaStr);

                //Revisa que la no este registrada en el proveedor
                bool exists = _proveedorDAO.IsMarcaExistsOnProveedor(proveedorId, marca);

                if (exists)
                    throw new RCVAsociationException("El proveedor ya se especializa en dicha marca");

                MarcaProveedor marcaEntity = new MarcaProveedor
                {
                    marcaId = Guid.NewGuid(),
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
        public bool AddAllMarcas(Guid proveedorId)
        {
            try
            {
                MarcaProveedor marcaEntity;

                //Borra los posible registros de marcas del proveedor
                _proveedorDAO.DeleteMarcasFromProveedor(proveedorId);
            
                //Genera una entidad marca que indique todas las marcas
                marcaEntity = new MarcaProveedor
                {
                    marcaId = Guid.NewGuid(),
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