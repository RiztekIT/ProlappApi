using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProlappApi.Models
{
    public class DetalleOrdenCarga
    {
       public long IdDetalleOrdenCarga { get; set; }
       public long IdOrdenCarga { get; set; }
       public long IdProveedor { get; set; }
       public string Proveedor { get; set; }
       public string PO { get; set; }
       public string IdProducto { get; set; }
       public string Lote { get; set; }
       public string Sacos { get; set; }
       public string PesoSaco { get; set; }
       public DateTime FechaMFG { get; set; }
       public DateTime FechaCaducidad { get; set; }
       public string Bodega { get; set; }
       public string USDA { get; set; }
       public string Shipper { get; set; }
       public string Pedimiento { get; set; }
       public string Estatus { get; set; }
    }
}