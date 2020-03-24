using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleOrdenDescarga
    {

        public long IdDetalleOrdenDescarga { get; set; }
        public int IdOrdenDescarga { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string Sacos { get; set; }
        public string PesoxSaco { get; set; }
        public string Lote { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string PO { get; set; }
        public DateTime FechaMFG { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Shipper { get; set; }
        public string USDA { get; set; }
        public string Pedimento { get; set; }
        public string Saldo { get; set; }
    }
}