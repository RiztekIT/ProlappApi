using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class OrdenTemporal
    {

        public long IdOrdenTemporal { get; set; }
        public long IdTarima { get; set; }
        public long IdOrdenCarga { get; set; }
        public long IdOrdenDescarga { get; set; }
        public string QR { get; set; }
        public string ClaveProducto { get; set; }
        public string Lote { get; set; }
        public string Sacos { get; set; }

    }
}