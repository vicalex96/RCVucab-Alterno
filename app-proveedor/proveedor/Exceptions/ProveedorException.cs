﻿using System;

namespace proveedor.Exceptions
{
    public class ProveedorException : Exception
    {
        public string Mensaje { get; set; }

        public Exception Excepcion { get; set; }

        public string CodigoError { get; set; }

        public string MensajeSoporte { get; set; }

        
        public ProveedorException(string _mensaje, Exception _excepcion, string _mensajesoporte, string _codigoError)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            CodigoError = _codigoError;
            MensajeSoporte = _mensajesoporte;
        }

       
        public ProveedorException(string _mensaje, string _mensajeSoporte, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
            MensajeSoporte = _mensajeSoporte;
        }

        /// <summary>
        /// Constructor del la excepción LotoAnimalitosException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        /// <param name="excepcion">Excepción original.</param>
        public ProveedorException(string _mensaje, Exception _excepcion)
        {
            Mensaje = _mensaje;
            Excepcion = _excepcion;
        }

        /// <summary>
        /// Constructor del la excepción LotoAnimalitosException.
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepción.</param>
        public ProveedorException(string _mensaje)
        {
            Mensaje = _mensaje;
        }
    }
}
