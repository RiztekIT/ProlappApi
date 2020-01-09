using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class PagoCFDI
    {
        public long Id { get; set; }
        public long IdReciboPago { get; set; }
        public long IdFactura { get; set; }
        public string UUID { get; set; }
        public string Cantidad { get; set; }
        public string NoParcialidad { get; set; }
        public string Saldo { get; set; }
    }
}