using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleTraspasoMercancia
    {

        public long IdDetalleTraspasoMercancia { get; set; }
        public long IdTraspasoMercancia { get; set; }
        public long IdDetalle { get; set; }
        public string Cbk { get; set; }
        public string Usda { get; set; }
        public long IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string PO { get; set; }
        public string Producto { get; set; }
        public string ClaveProducto { get; set; }
        public string Lote { get; set; }
        public string Sacos { get; set; }
        public string PesoxSaco { get; set; }
        public string PesoTotal { get; set; }
        public string Bodega { get; set; }
        public string CampoExtra3 { get; set; }
        public string CampoExtra4 { get; set; }


    }
}