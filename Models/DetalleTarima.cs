using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleTarima
    {
        public long IdDetalleTarima { get; set; }
        public string ClaveProducto { get; set; }
        public string Producto { get; set; }
        public string SacosTotales { get; set; }
        public string PesoxSaco { get; set; }
        public string Lote { get; set; }
        public string PesoTotal { get; set; }
        public string SacosxTarima { get; set; }
        public string TarimasTotales { get; set; }
        public string Bodega { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string PO { get; set; }
        public DateTime FechaMFG { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public string Shipper { get; set; }
        public string USDA { get; set; }
        public string Pedimento { get; set; }
        public string Estatus { get; set; }
    }
}