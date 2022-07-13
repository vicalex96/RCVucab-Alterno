using System;

namespace administracion.Exceptions
{
    public class RCVException : Exception
    {
        public string Mensaje { get; set; }

        public Exception? Excepcion { get; set; }

        public string? CodigoError { get; set; }

        public string? MensajeSoporte { get; set; }

        /// <summary>
        /// Constructor del la excepción RCVException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        /// <param name="mensajeSoporte">mensaje con informacion adicional</param>
        /// <param name="excepcion">Excepción original.</param>
        /// <param name="codigoError">Indica el numero de error que ocurrio</param>
        public RCVException(string _mensaje, Exception _excepcion, string _mensajesoporte, string _codigoError)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            CodigoError = _codigoError;
            MensajeSoporte = _mensajesoporte;
        }

        /// <summary>
        /// Constructor del la excepción RCVException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        /// <param name="mensajeSoporte">mensaje con informacion adicional</param>
        /// <param name="excepcion">Excepción original.</param>
        public RCVException(string _mensaje, string _mensajeSoporte, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            MensajeSoporte = _mensajeSoporte;
        }

        /// <summary>
        /// Constructor del la excepción RCVException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        ///<param name="excepcion">Excepción original.</param>
        public RCVException(string _mensaje, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
        }

        /// <summary>
        /// Constructor del la excepción RCVException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        public RCVException(string _mensaje)
        {
            Mensaje = _mensaje;
        }
    }
}
