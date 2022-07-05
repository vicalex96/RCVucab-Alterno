using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Conections.rabbit;
using administracion.Exceptions;

namespace administracion.BussinesLogic.LogicClasses
{
    public class TallerLogic: ITallerLogic
    {
        private readonly ITallerDAO _tallerDAO;
        private readonly IProductorRabbit _productorRabbit;

        public TallerLogic (ITallerDAO tallerDAO, IProductorRabbit productorRabbit)
        {
            _tallerDAO = tallerDAO;
            _productorRabbit = productorRabbit;
        }
        /// <summary>
        /// registra un taller en el sistema cumpliendo con la logica de negocio
        /// </summary>
        /// <param name="taller">DTO de registro con la data de taller</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool RegisterTaller (TallerRegisterDTO Taller)
        {
            try
            {
                //El nombre del local del Taller no puede estar vacio
                if(Taller.nombreLocal.ToLower() == "string" || 
                    Taller.nombreLocal.Count() == 0)
                {
                    throw new Exception("Error");
                }
                Taller tallerEntity = new Taller
                {
                    tallerId = Taller.Id,
                    nombreLocal = Taller.nombreLocal,
                };

                //registra el Taller en el sistema
                Guid Id = _tallerDAO.RegisterTaller(tallerEntity);

                //envia la informacion a la cola de mensajes
                _productorRabbit.SendMessage(
                    Routings.taller,
                    "registrar_taller",
                    Id.ToString()
                );

                return true;
            }
            catch(Exception ex)
            {
                throw new RCVException("Error al registrar el Taller", ex);
            }
        }

        /// <summary>
        /// Agrega una o todas las marcas al taller
        /// </summary>
        /// <param name="tallerId">Id del taller</param>
        /// <param name="marcaStr">nombre de la marca</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool AddMarca(Guid tallerId, string marcaStr)
        {
            try
            {
                Marca marca = new Marca();

                //Revisa que la marca introducida exita y la convierte de string a tipo Marca
                if(MarcaTaller.IsMarca(marcaStr))
                    marca = MarcaTaller.ConvertToMarca(marcaStr);
                else
                    throw new RCVException("La marca introducida no es valida");

                //Revisa que la no este registrada en el taller
                bool exists = _tallerDAO.IsMarcaExistsOnTaller(tallerId, marca);

                if (exists)
                    throw new RCVException("El taller ya se especializa en dicha marca");

                MarcaTaller marcaEntity = new MarcaTaller
                {
                    marcaId = Guid.NewGuid(),
                    tallerId = tallerId,
                    manejaTodas = false,
                    marca = marca
                };
                return _tallerDAO.AddMarca(marcaEntity);
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
        /// indica que in taller se especializara en todas las marcas
        /// </sumary>
        /// <param name="tallerId">Id del taller</param>
        /// <returns>boleano true si todo salio bien</returns>
        public bool AddAllMarcas(Guid tallerId)
        {
            try
            {
                MarcaTaller marcaEntity;

                //Borra los posible registros de marcas del taller
                bool success = _tallerDAO.DeleteMarcasFromTaller(tallerId);

                if(!success)
                    throw new RCVException("Ocurrio un problema al intentar Limpriar la lista de especificaciones");
            
                //Genera una entidad marca que indique todas las marcas
                marcaEntity = new MarcaTaller
                {
                    marcaId = Guid.NewGuid(),
                    tallerId = tallerId,
                    manejaTodas = true,
                };
                
                return _tallerDAO.AddMarca(marcaEntity);
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