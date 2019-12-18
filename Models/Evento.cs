using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Evento
    {
        public long IdEvento { get; set; }
        public long IdUsuario { get; set; }
        public string Movimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Autorizacion { get; set; }
     }
}