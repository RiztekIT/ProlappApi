using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Pedido
    {
        public long IdPedido { get; set; }
        public long IdCliente { get; set; }
        public string Folio { get; set; }
        public string Subtotal { get; set; }
        public string Descuento { get; set; }
        public string Total { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string OrdenDeCompra { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string CondicionesDePago { get; set; }
        public string Vendedor { get; set; }
        public string Estatus { get; set; }
        public string Usuario { get; set; }
        public long Factura { get; set; }
        public string LugarDeEntrega { get; set; }
        public string Moneda { get; set; }
        public string Prioridad { get; set;  }
        public string SubtotalDlls { get; set; }
        public string DescuentoDlls { get; set; }
        public string TotalDlls { get; set; }
        public string Flete { get; set; }


    }
}