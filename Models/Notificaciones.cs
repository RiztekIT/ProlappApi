using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Notificaciones
    {

        public long IdNotificacion {get; set;}
        public long Folio {get; set;}
        public long IdUsuario {get; set;}
        public string Usuario {get; set;}
        public string Mensaje {get; set;}
        public string ModuloOrigen {get; set;}
        public DateTime FechaEnvio {get; set;}

}
}