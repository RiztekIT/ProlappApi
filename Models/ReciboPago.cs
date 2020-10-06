using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class ReciboPago
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaPago { get; set; }
        public string FormaPago { get; set; }
        public string Moneda { get; set; }
        public string TipoCambio { get; set; }
        public string Cantidad { get; set; }
        public string Referencia { get; set; }
        public string UUID { get; set; }
        public string Tipo { get; set; }
        public string Certificado { get; set; }
        public string NoCertificado { get; set; }
        public string Cuenta { get; set; }
        public string CadenaOriginal { get; set; }
        public string SelloDigitalSAT { get; set; }
        public string SelloDigitalCFDI { get; set; }
        public string NoSelloSAT { get; set; }
        public string RFCPAC { get; set; }
        public string Estatus { get; set; }
        public string folio { get; set; }

    }
}