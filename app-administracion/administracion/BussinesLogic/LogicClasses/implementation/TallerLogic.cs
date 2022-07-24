using administracion.Persistence.DAOs;
using administracion.BussinesLogic.DTOs;
using administracion.Persistence.Entities;
using administracion.Conections.rabbit;
using administracion.Exceptions;
using administracion.Persistence.Enums;
using administracion.BussinesLogic.Mappers;

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
        public int RegisterTaller (TallerRegisterDTO taller)
        {
            try
            {
                //El nombre del local del Taller no puede estar vacio
                if(taller.nombreLocal.ToLower() == "string" || 
                    taller.nombreLocal.Count() == 0)
                {
                    throw new RCVInvalidFieldException("Error: el nombre del local no puede estar vacio o por defecto");
                }

                //registra el Taller en el sistema
                int result = _tallerDAO.RegisterTaller(
                                TallerMapper.MapToEntity(taller)
                            );

                /*//envia la informacion a la cola de mensajes
                _productorRabbit.SendMessage(
                    Routings.taller,
                    "registrar_taller",
                    Taller.Id.ToString()
                );
                */

                return result;
            }
            catch(RCVInvalidFieldException ex)
            {
                throw ex;
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
        public int AddMarca(Guid tallerId, string marcaStr)
        {
            try
            {
                MarcaName marca = new MarcaName();

                //Convierte la marca introducida al formato Marca si no se encuentra la marca lanza una excepcion
                marca = (MarcaName) Enum.Parse(typeof(MarcaName), marcaStr);

                //Revisa que la no este registrada en el taller
                bool exists = _tallerDAO.IsMarcaExistsOnTaller(tallerId, marca);

                if (exists)
                    throw new RCVAsociationException("El taller ya se especializa en dicha marca");

                MarcaTaller marcaEntity = new MarcaTaller
                {
                    Id = Guid.NewGuid(),
                    tallerId = tallerId,
                    manejaTodas = false,
                    marca = marca
                };
                return _tallerDAO.AddMarca(marcaEntity);
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
        /// indica que in taller se especializara en todas las marcas
        /// </sumary>
        /// <param name="tallerId">Id del taller</param>
        /// <returns>boleano true si todo salio bien</returns>
        public int AddAllMarcas(Guid tallerId)
        {
            try
            {
                MarcaTaller marcaEntity;

                //Borra los posible registros de marcas del taller
                _tallerDAO.DeleteMarcasFromTaller(tallerId);

                //Genera una entidad marca que indique todas las marcas
                marcaEntity = new MarcaTaller
                {
                    Id = Guid.NewGuid(),
                    tallerId = tallerId,
                    manejaTodas = true,
                };

                return _tallerDAO.AddMarca(marcaEntity);
            }
            catch (Exception ex)
            {
                throw new RCVException("Ocurrio algun error al intentar registrar el incidente", ex);
            }
        }
    
    }
}