using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleCotizacion
    {
        public long IdDetalleCotizacion { get; set; }
        public long IdCotizacion { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string Unidad { get; set; }
        public string PrecioUnitario { get; set; }
        public string PrecioUnitarioDlls { get; set; }
        public string Cantidad { get; set; }
        public string Importe { get; set; }
        public string ImporteDlls { get; set; }
        public string Observaciones { get; set; }
    }
}