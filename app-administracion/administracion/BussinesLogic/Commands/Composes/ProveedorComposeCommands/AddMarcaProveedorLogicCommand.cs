using administracion.BussinesLogic.DTOs;
using administracion.DataAccess.DAOs;
using administracion.DataAccess.Entities;
using administracion.DataAccess.Enums;
using administracion.Exceptions;

namespace administracion.BussinesLogic.Commands
{
    public class AddMarcaProveedorLogicCommand: Command<int>
    {
        private int _result;
        private readonly Guid _proveedorId;
        private readonly string _marca;

        public AddMarcaProveedorLogicCommand( Guid proveedorId, string marca)
        {
            _proveedorId = proveedorId;
            _marca = marca;
        }
        
        public override void Execute()
        {
            try
            {
                //convierte los datos ingresados en un objeto MarcaProveedor
                MarcaProveedor marcaProveedor = MarcaFactory.createMarcaProveedor(_proveedorId, _marca);

                //Revisa que la marca aun no este registrada en el proveedor
                IsMarcaExistsOnProveedorCommand isMarcaExists = new IsMarcaExistsOnProveedorCommand(_proveedorId, (MarcaName)Enum.Parse(typeof(MarcaName), _marca));
                isMarcaExists.Execute();

                if (isMarcaExists.GetResult())
                {
                    throw new RCVAsociationException("El proveedor ya se especializa en la marca introducida");
                }
                    
                AddMarcaProveedorCommand addMarcaProveedor = new AddMarcaProveedorCommand(marcaProveedor);
                addMarcaProveedor.Execute();

                _result = addMarcaProveedor.GetResult();
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

        public override int GetResult()
        {
            return _result!;
        }
    }
}