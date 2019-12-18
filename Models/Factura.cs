using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Factura
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaDeExpedicion { get; set; }
        public string LugarDeExpedicion { get; set; }
        public string Certificado { get; set; }
        public string NumeroDeCertificado { get; set; }
        public string UUID { get; set; }
        public string UsoDelCFDI { get; set; }
        public string Subtotal { get; set; }
        public string Descuento { get; set; }
        public string ImpuestosRetenidos { get; set; }
        public string ImpuestosTrasladados { get; set; }
        public string Total { get; set; }
        public string FormaDePago { get; set; }
        public string MetodoDePago { get; set; }
        public string Cuenta { get; set; }
        public string Moneda { get; set; }
        public string CadenaOriginal { get; set; }
        public string SelloDigitalSAT { get; set; }
        public string SelloDigitalCFDI { get; set; }
        public string NumeroDeSelloSAT { get; set; }
        public string RFCdelPAC { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string OrdenDeCompra { get; set; }
        public string TipoDeCambio { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public string CondicionesDePago { get; set; }
        public string Vendedor { get; set; }
        public string Estatus { get; set; }
        public string Ver { get; set; }
        public string Usuario { get; set; }

        //Detalle Factura

        public long IdDetalle { get; set; }
        public long IdFactura { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string Unidad { get; set; }
        public string ClaveSat { get; set; }
        public string PrecioUnitario { get; set; }
        public string Cantidad { get; set; }
        public string Importe { get; set; }
        public string ObservacionesConcepto { get; set; }
        public string TextoExtra { get; set; }





    }
}