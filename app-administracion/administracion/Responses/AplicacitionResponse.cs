using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace administracion.Responses
{
    public class ApplicationResponse<T>
    {
        public string Message { get; set; } = "";
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set;}
        public bool Success { get; set; } = true;
        public string Exception { get; set; } = "";
    }
}