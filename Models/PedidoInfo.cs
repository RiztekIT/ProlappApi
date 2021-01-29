using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class PedidoInfo
    {

        public long IdPedidoInfo { get; set; }
        public long IdPedido { get; set; }
        public string SeleccionManual { get; set; }
        public string Campo1 { get; set; }
        public string Campo2 { get; set; }
        public string Campo3 { get; set; }

    }
}