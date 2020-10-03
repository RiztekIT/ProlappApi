using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleCompra
    {

        public long IdDetalleCompra { get; set; }
        public long IdCompra { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string Cantidad { get; set; }
        public string PesoxSaco { get; set; }
        public string PrecioUnitario { get; set; }
        public string CostoTotal { get; set; }
        public string IVA { get; set; }
        public string Unidad { get; set; }
        public string Observaciones { get; set; }
        public string PrecioUnitarioDlls { get; set;}
        public string CostoTotalDlls { get; set;}
        public string IVADlls { get; set;}
}
}