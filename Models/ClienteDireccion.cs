using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class ClienteDireccion
    {
        public long IdDireccion { get; set; }
        public long IdCliente { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }

    }
}