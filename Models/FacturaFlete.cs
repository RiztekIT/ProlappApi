using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class FacturaFlete
    {
        public long IDFacturaFlete { get; set; }
        public string Fletera { get; set; }
        public string Factura { get; set; }
        public int IDPedido { get; set; }
        public int IDOrdenCarga { get; set; }
        public string Subtotal { get; set; }
        public string IVA { get; set; }
        public string Total { get; set; }
        public string Estatus { get; set; }

    }
}