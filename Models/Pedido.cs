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
        public string FechaVencimiento { get; set; }
        public string OrdenDeCompra { get; set; }
        public string FechaDeEntrega { get; set; }
        public string CondicionesDePago { get; set; }
        public string Vendedor { get; set; }
        public string Estatus { get; set; }
        public string Usuario { get; set; }
        public long Factura { get; set; }

        public string LugarDeEntrega { get; set; }


    }
}