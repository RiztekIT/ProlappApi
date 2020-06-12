using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Compras
    {
        public long IdCompra { get; set; }
        public long Folio { get; set; }
        public string PO { get; set; }
        public long IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Subtotal { get; set; }
        public string Total { get; set; }
        public string Descuento { get; set; }
        public string ImpuestosRetenidos { get; set; }
        public string ImpuestosTrasladados { get; set; }
        public string Moneda { get; set; }
        public string Observaciones { get; set; }
        public string TipoCambio { get; set; }
        public string CondicionesPago { get; set; }
        public string PesoTotal { get; set; }
        public string Estatus { get; set; }
        public long Factura { get; set; }
        public string Ver { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public DateTime FechaPromesa { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Comprador { get; set; }


    }
}