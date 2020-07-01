using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Calendario
    {
       public int IdCalendario { set; get; }
        public string NombreCalendario { set; get; }
        public string Modulo { set; get; }
        public string NombreUsuario { set; get; }
    }
}