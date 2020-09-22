using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Usuario
    {

        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contra { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public string Dispositivo { get; set; }




    }
}