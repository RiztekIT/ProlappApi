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
        public string ClaveProducto {get; set;}
        public string Stock { get; set; }
        public string DescripcionProducto { get; set; }
        public string Estatus { get; set; }
        public string UnidadMedida { get; set; }
        public int IVA { get; set; }
        public string CodigoBarras { get; set; }
        public string ClaveSAT { get; set; }

    }
}