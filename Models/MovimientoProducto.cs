using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class MovimientoProducto
    {
        public int IdMovimiento { set; get; }
        public string ClaveProducto { set; get; }
        public string Producto { set; get; }
        public string Marca { set; get; }
        public string Origen { set; get; }
        public string Presentacion { set; get; }
        public string Tipo { set; get; }
        public float Cantidad { set; get; }
    }
}