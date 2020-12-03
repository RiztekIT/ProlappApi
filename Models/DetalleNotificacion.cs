using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleNotificacion
    {

        public long IdDetalleNotificacion { get; set; }
        public long IdNotificacion { get; set; }
        public long IdUsuarioDestino { get; set; }
        public string UsuarioDestino { get; set; }
        public int BanderaLeido { get; set; }
        public DateTime FechaLeido { get; set; }

}
}