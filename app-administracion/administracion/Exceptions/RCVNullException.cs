using System;

namespace administracion.Exceptions
{
    public class RCVNullException : RCVException
    {        
        /// <summary>
        /// Constructor del la excepción RCVNullException
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        /// <param name="mensajeSoporte">mensaje con informacion adicional</param>
        /// <param name="excepcion">Excepción original.</param>
        /// <param name="codigoError">Indica el numero de error que ocurrio</param>
        public RCVNullException(string _mensaje, Exception _excepcion, string _mensajesoporte, string _codigoError) : base( _mensaje,_excepcion, _mensajesoporte,_codigoError)
        {
        }

        /// <summary>
        /// Constructor del la excepción RCVNullException
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        /// <param name="mensajeSoporte">mensaje con informacion adicional</param>
        /// <param name="excepcion">Excepción original.</param>
        public RCVNullException(string _mensaje, string _mensajeSoporte, Exception _excepcion):  base( _mensaje,  _mensajeSoporte, _excepcion)
        {

        }

        /// <summary>
        /// Constructor del la excepción RCVNullException
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        ///<param name="excepcion">Excepción original.</param>
        public RCVNullException(string _mensaje, Exception _excepcion) : base ( _mensaje, _excepcion)
        {
        }

        /// <summary>
        /// Constructor del la excepción RCVNullException
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        public RCVNullException(string _mensaje) : base (_mensaje)
        {
            Mensaje = _mensaje;
        }
    }
}
