using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class TraspasoTarima
    {
        public long IdTraspasoTarima { get; set; }
        public long IdOrigenTarima { get; set; }
        public long IdDestinoTarima { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string Lote { get; set; }
        public string Sacos { get; set; }
        public DateTime FechaTraspaso { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }

    }
}