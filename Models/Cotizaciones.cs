using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Cotizaciones
    {
        public long IdCotizaciones { get; set; }
        public long IdCliente { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string Subtotal { get; set; }
        public string Total { get; set; }
        public string Descuento { get; set; }
        public string SubtotalDlls { get; set; }
        public string TotalDlls { get; set; }
        public string DescuentoDlls { get; set; }
        public string Observaciones { get; set; }
        public string Vendedor { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaDeExpedicion { get; set; }
        public string Flete { get; set; }
        public long Folio { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
    }
}