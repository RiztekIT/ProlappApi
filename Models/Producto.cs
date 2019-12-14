using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class Producto
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; }
        public string PrecioVenta { get; set; }
        public string PrecioCosto { get; set; }
        public string Cantidad { get; set; }
    }
}