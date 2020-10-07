using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class OrdenTemporal
    {

        public long IdOrdenTemporal { get; set; }
        public long IdDetalleTarima { get; set; }
        public long IdOrdenCarga { get; set; }
        public long IdOrdenDescarga { get; set; }
        public string QR { get; set; }
        public string ClaveProducto { get; set; }
        public string Lote { get; set; }
        public string Sacos { get; set; }
        public string Producto { get; set; }
        public string PesoTotal { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Comentarios { get; set; }

    }
}